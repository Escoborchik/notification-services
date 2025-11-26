using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using SharedKernel;

namespace Framework;

[Route("api/[controller]")]
[ApiController]
public abstract class ApplicationController : ControllerBase
{
    public override OkObjectResult Ok([ActionResultObjectValue] object? value)
    {
        var result = Envelope.Ok(value);

        return base.Ok(result);
    }
}
