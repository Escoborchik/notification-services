using CSharpFunctionalExtensions;
using SharedKernel.ErrorsBase;

namespace NotificationGateway.Domain;

public record Content
{
    public string Subject { get; }
    public string Text { get; }

    

    public static Result<Content, Error> Create(string? subject, string? text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return Error.Failure("notification.invalid-content", "text is required.");

        return new Content(
            subject?.Trim() ?? string.Empty,
            text.Trim()
        );
    }

    private Content(string subject, string text)
    {
        Subject = subject;
        Text = text;
    }
}
