namespace NotificationGateway.Contracts.DTO;

public record RecipientDTO(string? Email, string? PhoneNumber, string? PushToken);
