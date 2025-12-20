using CSharpFunctionalExtensions;
using SharedKernel.ErrorsBase;

namespace NotificationGateway.Domain;

public record Recipient
{
    public string? Email { get; private set; }
    public string? Phone { get; private set; }
    public string? PushToken { get; private set; }

    private Recipient(string? email, string? phone, string? pushToken)
    {
        Email = email;
        Phone = phone;
        PushToken = pushToken;
    }

    public static Result<Recipient, Error> Create(
        string? email,
        string? phone,
        string? pushToken)
    {
        if (string.IsNullOrWhiteSpace(email)
            && string.IsNullOrWhiteSpace(phone)
            && string.IsNullOrWhiteSpace(pushToken))
        {
            return Error.Failure(
                "notification.invalid-recipient",
                "At least one recipient identifier must be specified.");
        }

        return new Recipient(email, phone, pushToken);
    }
}
