using System.Text.Json;
using LAB_2.Models;

namespace LAB_2
{
    public class WeatherGetter
    {
        private const string ApiUrl = "https://api.openweathermap.org/data/2.5/forecast?appid=d985bed89e513fbac38a5a2ee3f40fa3&q=Israel&units=metric&cnt=5"; 

        public async Task<WeatherForecast> GetData()
        {
            using (HttpClient client = new HttpClient())
            {
                var responseMessage = await client.GetStringAsync(ApiUrl);
                var weatherForecast = JsonSerializer.Deserialize<WeatherForecast>(responseMessage);
                return weatherForecast;
            }
        }
    }
}
