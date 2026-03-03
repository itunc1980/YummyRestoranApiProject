using ApiProject.WebUI.Dtos.CategoryDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApiProject.WebUI.ViewComponents.DashboardViewComponents
{
    public class _DashboardWidgetsComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _DashboardWidgetsComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            int r1,r2,r3, r4;
            Random rnd = new Random();
            r1 = rnd.Next(1, 40);
            r2 = rnd.Next(1, 40);
            r3 = rnd.Next(1, 40);
            r4 = rnd.Next(1, 40);

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7123/api/Reservations/GetTotalReservationCount");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            ViewBag.v1 = jsonData;
            ViewBag.r1 = r1;

            var client2 = _httpClientFactory.CreateClient();
            var responseMessage2 = await client2.GetAsync("https://localhost:7123/api/Reservations/GetTotalCustomerCount");
            var jsonData2 = await responseMessage2.Content.ReadAsStringAsync();
            ViewBag.v2 = jsonData2;
            ViewBag.r2 = r2;

            var client3 = _httpClientFactory.CreateClient();
            var responseMessage3 = await client3.GetAsync("https://localhost:7123/api/Reservations/GetPendingReservation");
            var jsonData3 = await responseMessage3.Content.ReadAsStringAsync();
            ViewBag.v3 = jsonData3;
            ViewBag.r3 = r3;

            var client4 = _httpClientFactory.CreateClient();
            var responseMessage4 = await client4.GetAsync("https://localhost:7123/api/Reservations/GetApprovedReservation");
            var jsonData4 = await responseMessage4.Content.ReadAsStringAsync();
            ViewBag.v4 = jsonData4;
            ViewBag.r4 = r4;

            return View();
        }
    }
}
