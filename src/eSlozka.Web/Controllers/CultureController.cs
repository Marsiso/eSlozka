using eSlozka.Domain.Constants;
using eSlozka.Web.Controllers.Common;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace eSlozka.Web.Controllers;

[Route("[Controller]/[Action]")]
public class CultureController : WebControllerBase<CultureController>
{
    public CultureController(ILogger<CultureController> logger) : base(logger)
    {
    }

    public IActionResult SetCulture(string? culture, string? redirectUri)
    {
        if (!string.IsNullOrWhiteSpace(culture))
        {
            var cultureInfo = new RequestCulture(culture);
            HttpContext.Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(cultureInfo));
        }

        return LocalRedirect(redirectUri ?? Routes.Home);
    }
}