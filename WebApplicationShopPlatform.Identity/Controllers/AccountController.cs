using Microsoft.AspNetCore.Mvc;

namespace WebApplicationShopPlatform.Identity.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
