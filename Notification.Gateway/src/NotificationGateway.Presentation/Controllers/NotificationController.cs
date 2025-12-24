using Framework;
using Microsoft.AspNetCore.Mvc;
using NotificationGateway.Application.Features.Commands.CreateNotification;
using NotificationGateway.Application.Features.Queries;
using NotificationGateway.Contracts.DTO;
using NotificationGateway.Contracts.Requests;

namespace NotificationGateway.Presentation.Controllers
{
    public class NotificationController : ApplicationController
    {
        /// <summary>
        /// Получение информацию об уведомлении
        /// </summary>

        [HttpGet()]
        public async Task<ActionResult<NotificationDTO>> Get(
        [FromQuery] Guid notificationId,
        [FromServices] GetNotificationQueryHandler handler,
        CancellationToken cancellationToken)
        {
            var query = new GetNotificationQuery(
                notificationId);

            var result = await handler.Execute(query, cancellationToken);

            if (result.IsFailure)
            {
                return result.Error.ToResponse();
            }

            return Ok(result.Value);
        }

        /// <summary>
        /// Отправить уведомление
        /// </summary>

        [HttpPost()]
        public async Task<ActionResult<Guid>> SendNotification(
        [FromServices] CreateNotificationCommandHandler handler,
        [FromBody] CreateNotificationRequest request,
        CancellationToken cancellationToken)
        {
            var command = CreateNotificationCommand.FromRequest(request);

            var result = await handler.Execute(command, cancellationToken);

            if (result.IsFailure)
            {
                return result.Error.ToResponse();
            }

            return Ok(result.Value);
        }
    }
}
