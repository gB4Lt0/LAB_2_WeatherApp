using LAB_2.Models;

namespace LAB_2
{
    public class Program
    {
        private static WeatherGetter? _weatherGetter;
        private static DBManager? _dbManager;
        public static async Task Main(string[] args)
        {
            _weatherGetter = new WeatherGetter();
            _dbManager = new DBManager();
            _dbManager.CreateTableIfNotExist();

            await HandleInput();
        }

        private static async Task HandleInput()
        {
            while (true)
            {
                await Console.Out.WriteLineAsync("Enter: get, print, exit");
                //Console.WriteLine("Enter: get, print, exit");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "get":
                        GetData();
                        //Console.WriteLine("Data received");
                        await Console.Out.WriteLineAsync("Data received");
                        break;
                    case "print":
                        PrintData();
                        break;
                    case "exit":
                        return;
                    default:
                        break;
                }
            }
        }

        private static void GetData()
        {
            WeatherForecast wf = _weatherGetter.GetData().Result;
            _dbManager.InsertData(new MyWeatherForecast(
                wf.city.name,
                wf.list[0].dt_txt,
                wf.list[0].main.temp,
                wf.list[0].wind.speed,
                wf.list[0].weather[0].main,
                wf.list[0].weather[0].description
                ));
        }

        private static void PrintData()
        {
            List<MyWeatherForecast> data = _dbManager.ReadData();
            foreach (var dataItem in data)
            {
                Console.WriteLine(dataItem);
            }
        }
    }
}