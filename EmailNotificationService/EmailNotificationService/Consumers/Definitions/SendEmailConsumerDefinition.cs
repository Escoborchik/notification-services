using MassTransit;

namespace EmailNotificationService.Consumers.Definitions;

public class SendEmailConsumerDefinition : ConsumerDefinition<EmailNotificationEventConsumer>
{
    protected override void ConfigureConsumer(
        IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<EmailNotificationEventConsumer> consumerConfigurator,
        IRegistrationContext context)
    {
        endpointConfigurator.UseMessageRetry(
            c => c.Incremental(3, TimeSpan.FromSeconds(3), TimeSpan.FromSeconds(5)));
    }
}
 