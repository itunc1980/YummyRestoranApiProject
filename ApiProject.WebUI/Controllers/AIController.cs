using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace ApiProject.WebUI.Controllers
{
    public class AIController : Controller
    {
        public IActionResult CreateRecipeWithAI()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRecipeWithAI(string prompt)
        {
            var apiKey = "";

            var url = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent";

            using var client = new HttpClient();

            client.DefaultRequestHeaders.Add("x-goog-api-key", apiKey);

            var requestData = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[]
                        {
                            new { text = "Sen bir restoran için yemek önerilerini yapan bir yapay zeka aracısın. Amacımız kullanıcı tarafından girilen malzemelere göre yemek tarifi önerisinde bulunmak. " + prompt }
                        }
                    }
                }
            };

            var response = await client.PostAsJsonAsync(url, requestData);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<GeminiResponse>();

                var content = result.candidates[0].content.parts[0].text;
                ViewBag.Recipe = content;
            }
            else
            {
                ViewBag.Error = "Bir hata oluştu: " + response.StatusCode;
            }

            return View("CreateRecipeWithAI");
        }

        public class GeminiResponse
        {
            public List<Candidate> candidates { get; set; }
        }

        public class Candidate
        {
            public Content content { get; set; }
        }

        public class Content
        {
            public List<Part> parts { get; set; }
        }

        public class Part
        {
            public string text { get; set; }
        }
    }
}