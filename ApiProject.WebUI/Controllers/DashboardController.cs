using Microsoft.AspNetCore.Mvc;

namespace ApiProject.WebUI.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
