using LAB_2.Models;
using System.Diagnostics.Metrics;
using System.Text.Json;

namespace LAB_2
{
    public class Program
    {
        private static DataBaseManager? _dbManager;
        public static async Task Main(string[] args)
        {
            _dbManager = new DataBaseManager();
            _dbManager.CreateTableIfNotExist();
            while (true)
            {
                Console.WriteLine("Enter what you want to do: ");
                Console.WriteLine("1 - Get the forecast for today for a specific city");
                Console.WriteLine("2 - to display database data");

                string selectedOption = Console.ReadLine();
                switch (selectedOption)
                {
                    case "1":
                        Console.Write("Enter name of the city: ");
                        string city = Console.ReadLine();
                        await GetData(city);
                        Console.WriteLine();
                        break;
                    case "2":
                        PrintData();
                        Console.WriteLine();
                        break;
                    default:
                        break;
                }

            };
        }

        private static async Task GetData(string city)
        {
            using (HttpClient client = new HttpClient())
            {
                string apiUrl = "https://api.openweathermap.org/data/2.5/forecast?appid=d985bed89e513fbac38a5a2ee3f40fa3&q=" + city + "&units=metric&cnt=5";
                var responseMessage = await client.GetStringAsync(apiUrl);
                WeatherForecast weatherForecast = JsonSerializer.Deserialize<WeatherForecast>(responseMessage);
                MyWeatherForecast newWeatherData = new MyWeatherForecast(
                    weatherForecast.city.name,
                    weatherForecast.list[0].dt_txt,
                    weatherForecast.list[0].main.temp,
                    weatherForecast.list[0].wind.speed,
                    weatherForecast.list[0].weather[0].main,
                    weatherForecast.list[0].weather[0].description);
                _dbManager.AddData(newWeatherData);

                Console.WriteLine("New Weather Data:");
                Console.WriteLine(newWeatherData);
            }

        }

        private static void PrintData()
        {
            List<MyWeatherForecast> data = _dbManager.GetData();
            foreach (var dataItem in data)
            {
                Console.WriteLine(dataItem);
            }
        }
    }
}