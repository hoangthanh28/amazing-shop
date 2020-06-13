using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MyShop.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task Login(string returnUrl = null)
        {
            if (returnUrl == null)
                returnUrl = "/";
            if (User.Identity == null || !User.Identity.IsAuthenticated)
            {
                var props = new AuthenticationProperties
                {
                    RedirectUri = returnUrl,
                    Items =
                        {
                            { "scheme", "oidc" },
                            { "returnUrl", returnUrl }
                        }
                };
                await HttpContext.ChallengeAsync(props);
            }
        }

        [Authorize]
        public async Task Logout()
        {
            await HttpContext.SignOutAsync("Cookies");
            await HttpContext.SignOutAsync("oidc");
        }

    }

}
