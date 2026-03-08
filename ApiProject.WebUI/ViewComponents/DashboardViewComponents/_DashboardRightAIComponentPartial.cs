using Microsoft.AspNetCore.Mvc;

namespace ApiProject.WebUI.ViewComponents.DashboardViewComponents
{
    public class _DashboardRightAIComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
