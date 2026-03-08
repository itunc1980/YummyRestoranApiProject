using System.Net.Http.Headers;
using System.Text;
using ApiProject.WebUI.Dtos.AISuggestionDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApiProjeKampi.WebUI.ViewComponents.DashboardViewComponents
{
    public class _DashboardAIDailyMenuSuggestionComponentPartial : ViewComponent
    {
        // Gemini API anahtarını buraya ekleyin
        private const string GEMINI_API_KEY = "AIzaSyBnG1Cq3PZ16gU_hS8pwpr2jx-bkxAU2I4";
        private const string GEMINI_MODEL = "gemini-2.5-flash";

        private readonly IHttpClientFactory _httpClientFactory;
        public _DashboardAIDailyMenuSuggestionComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();

            // Gemini API Endpoint'i (API Key sorgu parametresi olarak gönderilir)
            string url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent";

            string prompt = @"
                4 farklı dünya mutfağından tamamen rastgele günlük menü oluştur.
                Seçilecek Ülkeler: Türkiye, Fransa, Almanya, İtalya, İspanya, Portekiz, Bulgaristan, Gürcistan, Yunanistan, İran, Çin.
                
                KURALLAR:
                - Mutlaka 4 FARKLI ülke seç.
                - ISO Country Code zorunlu (TR, IT, FR vb.).
                - Tüm içerik Türkçe olsun.
                - Sadece saf JSON döndür, markdown (```json) kullanma.

                JSON Yapısı:
                [
                  {
                    ""Cuisine"": ""X Mutfağı"",
                    ""CountryCode"": ""XX"",
                    ""MenuTitle"": ""Günlük Menü"",
                    ""Items"": [
                      { ""Name"": ""Yemek 1"", ""Description"": ""Açıklama"", ""Price"": 100 },
                      { ""Name"": ""Yemek 2"", ""Description"": ""Açıklama"", ""Price"": 120 }
                    ]
                  }
                ]";

            // Gemini için request body yapısı OpenAI'dan farklıdır
            var body = new
            {
                contents = new[]
                {
                    new { parts = new[] { new { text = prompt } } }
                },
                generationConfig = new
                {
                    // Bu ayar çıktının kesinlikle JSON olmasını sağlar
                    response_mime_type = "application/json"
                }
            };

            var jsonBody = JsonConvert.SerializeObject(body);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, content);
            var responseJson = await response.Content.ReadAsStringAsync();

            List<MenuSuggestionDto> menus = new();

            try
            {
                // Gemini response hiyerarşisi: candidates[0] -> content -> parts[0] -> text
                dynamic result = JsonConvert.DeserializeObject(responseJson);
                string aiContent = result.candidates[0].content.parts[0].text;

                menus = JsonConvert.DeserializeObject<List<MenuSuggestionDto>>(aiContent);
            }
            catch (Exception)
            {
                // Hata durumunda boş liste döner
                menus = new List<MenuSuggestionDto>();
            }

            return View(menus);
        }
    }
}