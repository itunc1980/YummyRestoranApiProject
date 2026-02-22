using Microsoft.AspNetCore.Mvc;

namespace ApiProject.WebUI.ViewComponents.AdminLayoutViewComponents
{
    public class _RightSettingsAdminLayoutComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
