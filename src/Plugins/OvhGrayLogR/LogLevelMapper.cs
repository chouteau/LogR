﻿using Serilog.Events;

namespace OvhGrayLogR;

/// <summary>
/// Recopié de https://github.com/whir1/serilog-sinks-graylog/blob/master/src/Serilog.Sinks.Graylog.Core/Helpers/LogLevelMapper.cs car c'est en internal
/// </summary>
internal static class LogLevelMapper
{
    private static readonly Dictionary<LogEventLevel, int> LogLevelMap = new()
    {
        [LogEventLevel.Verbose] = 7,
        [LogEventLevel.Debug] = 7,
        [LogEventLevel.Information] = 6,
        [LogEventLevel.Warning] = 4,
        [LogEventLevel.Error] = 3,
        [LogEventLevel.Fatal] = 0
    };

    /// <summary>
    /// Gets the mapped level.
    /// </summary>
    /// <param name="logEventLevel">The log event level.</param>
    /// <returns>Syslog level</returns>
    /// <remarks>
    /// SyslogLevels:
    /// 0 Emergency: system is unusable
    /// 1 Alert: action must be taken immediately
    /// 2 Critical: critical conditions
    /// 3 Error: error conditions
    /// 4 Warning: warning conditions
    /// 5 Notice: normal but significant condition
    /// 6 Informational: informational messages
    /// 7 Debug: debug-level messages
    /// </remarks>
    internal static int GetMappedLevel(LogEventLevel logEventLevel)
    {
        return LogLevelMap[logEventLevel];
    }
}
