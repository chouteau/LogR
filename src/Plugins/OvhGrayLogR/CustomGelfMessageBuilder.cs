﻿using Newtonsoft.Json.Linq;
using Serilog.Events;
using Serilog.Sinks.Graylog.Core;
using Serilog.Sinks.Graylog.Core.Extensions;
using Serilog.Sinks.Graylog.Core.MessageBuilders;
using System.IO;

namespace OvhGrayLogR;

internal class CustomGelfMessageBuilder : IMessageBuilder
{
    private readonly string _hostName;
    private readonly string _ovhKey;
    private const string DefaultGelfVersion = "1.1";
    protected GraylogSinkOptionsBase Options { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomGelfMessageBuilder"/> class.
    /// </summary>
    /// <param name="hostName">Name of the host.</param>
    /// <param name="options">The options.</param>
    internal CustomGelfMessageBuilder(string hostName, GraylogSinkOptionsBase options, string ovhKey)
    {
        _hostName = hostName;
        Options = options;
        _ovhKey = ovhKey;
    }

    /// <summary>
    /// Builds the specified log event.
    /// </summary>
    /// <param name="logEvent">The log event.</param>
    /// <returns></returns>
    public virtual JObject Build(LogEvent logEvent)
    {
        string message = logEvent.RenderMessage();
        string shortMessage = message.Truncate(Options.ShortMessageMaxLength);

        var gelfMessage = new GelfMessage
        {
            Version = DefaultGelfVersion,
            Host = Options.Host ?? _hostName,
            ShortMessage = shortMessage,
            Timestamp = logEvent.Timestamp.ConvertToNix(),
            Level = LogLevelMapper.GetMappedLevel(logEvent.Level),
            StringLevel = logEvent.Level.ToString(),
            Facility = Options.Facility,
            FullMessage = message
        };

        var jsonObject = JObject.FromObject(gelfMessage);
        foreach (KeyValuePair<string, LogEventPropertyValue> property in logEvent.Properties)
        {
            AddAdditionalField(jsonObject, property);
        }

        if (Options.IncludeMessageTemplate)
        {
            string messageTemplate = logEvent.MessageTemplate.Text;
            jsonObject.Add($"_{Options.MessageTemplateFieldName}", messageTemplate);
        }

        jsonObject.Add("_X-OVH-TOKEN", _ovhKey);

        return jsonObject;
    }

    private void AddAdditionalField(IDictionary<string, JToken> jObject, KeyValuePair<string, LogEventPropertyValue> property, string memberPath = "")
    {
        string key = string.IsNullOrEmpty(memberPath) ? property.Key : $"{memberPath}.{property.Key}";

        switch (property.Value)
        {
            case ScalarValue scalarValue:
                if (key.Equals("id", StringComparison.OrdinalIgnoreCase))
                {
                    key = "id_";
                }

                if (!key.StartsWith("_", StringComparison.OrdinalIgnoreCase))
                {
                    key = "_" + key;
                }

                if (scalarValue.Value == null)
                {
                    jObject.Add(key, null);
                    break;
                }

                var shouldCallToString = ShouldCallToString(scalarValue.Value.GetType());

                var value = JToken.FromObject(shouldCallToString ? scalarValue.Value.ToString() : scalarValue.Value);

                jObject.Add(key, value);
                break;
            case SequenceValue sequenceValue:
                var sequenceValueString = RenderPropertyValue(sequenceValue);
                jObject.Add(key, sequenceValueString);
                break;
            case StructureValue structureValue:
                foreach (LogEventProperty logEventProperty in structureValue.Properties)
                {
                    AddAdditionalField(jObject,
                                       new KeyValuePair<string, LogEventPropertyValue>(logEventProperty.Name, logEventProperty.Value)
                                       , key);
                }
                break;
            case DictionaryValue dictionaryValue:
                foreach (KeyValuePair<ScalarValue, LogEventPropertyValue> dictionaryValueElement in dictionaryValue.Elements)
                {
                    var renderedKey = RenderPropertyValue(dictionaryValueElement.Key);
                    AddAdditionalField(jObject, new KeyValuePair<string, LogEventPropertyValue>(renderedKey, dictionaryValueElement.Value), key);
                }
                break;
            default:
                break;
        }
    }

    private static bool ShouldCallToString(Type type)
    {
        bool isNumeric = type.IsNumericType();
        if (type == typeof(DateTime) || isNumeric)
        {
            return false;
        }
        return true;
    }

    private static string RenderPropertyValue(LogEventPropertyValue propertyValue)
    {
        using (var tw = new StringWriter())
        {
            propertyValue.Render(tw);
            string result = tw.ToString();
            result = result.Trim('"');
            return result;
        }
    }
}
