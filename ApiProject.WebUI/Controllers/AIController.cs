using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
// System.Text ve Newtonsoft.Json kütüphanelerini sildik, onlara artık gerek yok.

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

            // API Key'i URL'den çıkardık, daha temiz bir URL oldu.
            var url = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent";

            using var client = new HttpClient();

            // Hocanın Bearer token eklediği gibi, biz de Gemini'nin beklediği yetkilendirme başlığını (Header) ekliyoruz.
            client.DefaultRequestHeaders.Add("x-goog-api-key", apiKey);

            // Gemini'nin beklediği JSON yapısı (Hocanın requestData yapısına benzer şekilde)
            var requestData = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[]
                        {
                            new { text = "Sen bir restoran için yemek önerilerini yapan bir yapay zeka aracısın. Amacımız kullanıcı tarafından girilen malzemelere göre yemek tarifi önerisinde bulunmak. Malzemeler: " + prompt }
                        }
                    }
                }
            };

            // Hocanın kodundaki gibi PostAsJsonAsync kullanıyoruz (Manuel StringContent ve Serialize işlemine gerek kalmadı)
            var response = await client.PostAsJsonAsync(url, requestData);

            if (response.IsSuccessStatusCode)
            {
                // dynamic yerine hocanın yaptığı gibi kendi yazdığımız sınıfları (class) kullanarak veriyi okuyoruz
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

        // --- HOCANIN OpenAIResponse SINIFLARINA KARŞILIK GELEN GEMINI SINIFLARI ---
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