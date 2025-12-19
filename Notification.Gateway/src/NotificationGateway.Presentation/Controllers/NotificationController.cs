using Framework;
using Microsoft.AspNetCore.Mvc;
using NotificationGateway.Application.Commands.CreateNotification;
using NotificationGateway.Contracts.Requests;

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
        /// Отправить уведомление
        /// </summary>

        [HttpPost()]
        public async Task<ActionResult<Guid>> SendNotification(
        [FromServices] SendNotificationCommandHandler handler,
        [FromBody] SendNotificationRequest request,
        CancellationToken cancellationToken)
        {
            var command = SendNotificationCommand.FromRequest(request);

            var result = await handler.Execute(command, cancellationToken);

            if (result.IsFailure)
            {
                return result.Error.ToResponse();
            }

            return Ok(result.Value);
        }
    }
}
