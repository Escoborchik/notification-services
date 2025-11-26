using Framework;
using Microsoft.AspNetCore.Mvc;
using NotificationGateway.Application.Commands.CreateNotification;
using NotificationGateway.Contracts;

namespace NotificationGateway.Presentation.Controllers
{
    public class NotificationController : ApplicationController
    {
        /// <summary>
        /// Получение статуса уведомления
        /// </summary>

        //[HttpGet()]
        //public async Task<ActionResult<string>> Get(
        //[FromQuery] Guid notificationId,
        //[FromServices] GetOrganizationQueryHandler handler,
        //CancellationToken cancellationToken)
        //{
        //    var query = new GetOrganizationQuery(
        //        organizationId);

        //    var result = await handler.Execute(query, cancellationToken);

        //    if (result.IsFailure)
        //    {
        //        return result.Error.ToResponse();
        //    }

        //    return Ok(result.Value);
        //}

        /// <summary>
        /// Создание уведомления
        /// </summary>

        [HttpPost()]
        public async Task<ActionResult<Guid>> Create(
        [FromServices] CreateNotificationCommandHandler handler,
        [FromBody] CreateNotificationRequest request,
        CancellationToken cancellationToken)
        {
            var command = new CreateNotificationCommand(request.Channel, request.Recipient);

            var result = await handler.Execute(command, cancellationToken);

            if (result.IsFailure)
            {
                return result.Error.ToResponse();
            }

            return Ok(result.Value);
        }
    }
}
