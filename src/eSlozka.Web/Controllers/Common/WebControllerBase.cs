using Microsoft.AspNetCore.Mvc;

namespace eSlozka.Web.Controllers.Common;

[ApiController]
public class WebControllerBase<TController> : ControllerBase where TController : class
{
    protected readonly ILogger<TController> Logger;

    public WebControllerBase(ILogger<TController> logger)
    {
        Logger = logger;
    }
}