using BlogWithMyPrincess.Entities;

namespace BlogWithMyPrincess.Helpers;

public class EmailChecker
{
    private readonly string _email;

    public string Email => _email;
    public bool IsValid { get; }

    public EmailChecker(string email)
    {
        _email = email ?? string.Empty;
        IsValid = IsValidEmail(_email);
    }

    private static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        // Usa la validación segura de .NET
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
}