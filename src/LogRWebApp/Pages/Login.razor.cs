using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace LogRWebApp.Pages;

public class LoginForm
{
    [Required]
    public string Key { get; set; }
}

public partial class Login
{
    [Inject] 
    Services.LogRWebAppSettings LogRWebAppSettings { get; set; }
    [Inject] 
    NavigationManager NavigationManager { get; set; }
    [Inject] 
    Services.AdminLoginContext AdminLoginContext { get; set; }

    LoginForm LoginForm { get; set; } = new();
    LoginValidator LoginValidator { get; set; } = new();

    public void Validate()
    {
        var errors = new Dictionary<string, List<string>>();
        if (LogRWebAppSettings.AdminKey != LoginForm.Key)
        {
            errors.Add(nameof(LoginForm.Key), new List<string> { "invalid key" });
        }

        if (errors.Any())
        {
            LoginValidator.DisplayErrors(errors);
            return;
        }
        var token = Guid.NewGuid();
        AdminLoginContext.AddToken(token);
        NavigationManager.NavigateTo($"/?Token={token}", true);
    }

}
