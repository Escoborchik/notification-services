using CSharpFunctionalExtensions;
using SharedKernel.ErrorsBase;

namespace NotificationGateway.Domain;

public record Content
{
    public string Subject { get; }
    public string Body { get; }

    
    public static Result<Content, Error> Create(string subject, string body)
    {
        if (string.IsNullOrWhiteSpace(subject))
            return Error.Failure("notification.invalid-content", "subject is required.");

        if (string.IsNullOrWhiteSpace(body))
            return Error.Failure("notification.invalid-content", "body is required.");

        return new Content(subject, body);
    }

    private Content(string subject, string body)
    {
        Subject = subject;
        Body = body;
    }
}
