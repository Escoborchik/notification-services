namespace EmailNotificationService.Options;

public sealed class SmtpOptions
{
    public const string SMTP = "Smtp";
    public string Host { get; init; } = null!;
    public int Port { get; init; }
    public bool UseSsl { get; init; }

    public string FromEmail { get; init; } = null!;
    public string FromName { get; init; } = null!;

    public string? UserName { get; init; }
    public string? Password { get; init; }
}