using FluentAssertions;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;
using NotificationGateway.Application.Features.Commands.CreateNotification;
using NotificationGateway.Application.Interfaces;
using NotificationGateway.Contracts.DTO;
using NotificationGateway.Domain;
using NotificationGateway.Domain.Enums;
using Notifications.Contracts.Events;
using Xunit.Abstractions;

namespace Notification.Gateway.IntegrationTest;

public class CreateNotification :
    IClassFixture<IntegrationFixture>,
    IDisposable
{
    private readonly IServiceScope _scope;

    public CreateNotification(IntegrationFixture fixture)
    {
        var factory = new TestWebFactory(fixture);
        _scope = factory.Services.CreateScope();
    }

    private CreateNotificationCommandHandler Handler =>
        _scope.ServiceProvider.GetRequiredService<CreateNotificationCommandHandler>();

    private INotificationRepository Repository =>
        _scope.ServiceProvider.GetRequiredService<INotificationRepository>();

    public void Dispose()
    {
        _scope.Dispose();
    }

    [Fact]
    public async Task Should_create_email_notification_and_queue_it()
    {
        // Arrange
        var command = new CreateNotificationCommand(
            NotificationChannel.Email,
            NotificationType.Welcome,
            new ContentDTO("Hello", "World"),
            new RecipientDTO("test@test.com", null, null));

        // Act
        var result = await Handler.Execute(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();

        var notification = await Repository.FirstOrDefaultAsync(n => n.Id == result.Value, CancellationToken.None);
        notification.Should().NotBeNull();
        notification!.Status.Should().Be(NotificationQueueStatus.Queued);
    }

    [Fact]
    public async Task Should_fail_when_email_is_missing_for_email_channel()
    {
        // Arrange
        var command = new CreateNotificationCommand(
            NotificationChannel.Email,
            NotificationType.Welcome,
            new ContentDTO("Hello", "World"),
            new RecipientDTO(null, null, null));

        // Act
        var result = await Handler.Execute(command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(e =>
            e.Code == "notification.invalid-recipient");
    }

    [Fact]
    public async Task Should_fail_when_channel_is_not_supported()
    {
        // Arrange
        var command = new CreateNotificationCommand(
            (NotificationChannel)999,
            NotificationType.Welcome,
            new ContentDTO("Hello", "World"),
            new RecipientDTO("test@test.com", null, null));

        // Act
        var result = await Handler.Execute(command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Contain(e =>
            e.Code == "notification.unsupported-channel");
    }

    [Fact]
    public async Task Should_save_notification_with_pending_status_before_queueing()
    {
        // Arrange
        var command = new CreateNotificationCommand(
            NotificationChannel.Email,
            NotificationType.Welcome,
            new ContentDTO("Hello", "World"),
            new RecipientDTO("test@test.com", null, null));

        // Act
        var result = await Handler.Execute(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();

        var notification = await Repository.FirstOrDefaultAsync(
            n => n.Id == result.Value, CancellationToken.None);

        notification.Should().NotBeNull();
        notification!.DeliveryStatus.Should().Be(NotificationDeliveryStatus.None);
    }

    [Fact]
    public async Task Should_publish_email_notification_event()
    {
        // Arrange
        var harness = _scope.ServiceProvider.GetRequiredService<ITestHarness>();

        var command = new CreateNotificationCommand(
            NotificationChannel.Email,
            NotificationType.Welcome,
            new ContentDTO("Hello", "World"),
            new RecipientDTO("test@test.com", null, null));

        // Act
        var result = await Handler.Execute(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();

        (await harness.Published.Any<EmailNotificationEvent>())
            .Should().BeTrue();
    }
}
