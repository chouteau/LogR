﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace LogRWebApp.Pages;

public class LoginValidator : ComponentBase
{
    private ValidationMessageStore _messageStore;
    [CascadingParameter]
    public EditContext CurrentEditContext { get; set; }

    protected override void OnInitialized()
    {
        if (CurrentEditContext == null)
        {
            throw new InvalidOperationException();
        }
        _messageStore = new(CurrentEditContext);
        CurrentEditContext.OnValidationRequested += (s, arg) => _messageStore.Clear();
    }

    public void DisplayErrors(Dictionary<string, List<string>> errors)
    {
        foreach (var error in errors)
        {
            _messageStore.Add(CurrentEditContext.Field(error.Key), error.Value);
        }
        CurrentEditContext.NotifyValidationStateChanged();
    }
}
