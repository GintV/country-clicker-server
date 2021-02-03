using Microsoft.AspNetCore.Mvc;

namespace CountryClicker.Client.Controllers
{
    public class AuthorizationController: Controller
    {
        public IActionResult AccessDenied()
        {
            return View(null);
        }
    }
}
