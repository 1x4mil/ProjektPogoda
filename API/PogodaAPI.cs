using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ProjektPogoda.Models;

namespace ProjektPogoda.Services
{
    public class PogodaAPI
    {
        private readonly HttpClient _httpClient = new();
        private const string ApiKey = "0c103dcdc822989dee6cca305bad5f9b";
        private const string BaseUrl = "https://api.openweathermap.org/data/2.5/weather";

        public async Task<Pogoda?> PobierzPogodeAsync(string miasto)
        {
            var url = $"{BaseUrl}?q={miasto}&appid={ApiKey}&units=metric&lang=pl";

            try
            {
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                    return null;

                var json = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(json);

                var root = doc.RootElement;

                return new Pogoda
                {
                    Miasto = root.GetProperty("name").GetString() ?? miasto,
                    Kraj = root.GetProperty("sys").GetProperty("country").GetString(),
                    CzasPomiaru = DateTimeOffset.FromUnixTimeSeconds(root.GetProperty("dt").GetInt64()).LocalDateTime,

                    Temperatura = root.GetProperty("main").GetProperty("temp").GetDouble(),
                    Odczuwalna = root.GetProperty("main").GetProperty("feels_like").GetDouble(),
                    Min = root.GetProperty("main").GetProperty("temp_min").GetDouble(),
                    Max = root.GetProperty("main").GetProperty("temp_max").GetDouble(),

                    Opis = root.GetProperty("weather")[0].GetProperty("description").GetString() ?? "",
                    Cisnienie = root.GetProperty("main").GetProperty("pressure").GetDouble(),
                    Wilgotnosc = root.GetProperty("main").GetProperty("humidity").GetInt32(),

                    PredkoscWiatru = root.GetProperty("wind").GetProperty("speed").GetDouble(),
                    Zachmurzenie = root.GetProperty("clouds").GetProperty("all").GetInt32()
                };
            }
            catch
            {
                return null;
            }
        }
    }
}