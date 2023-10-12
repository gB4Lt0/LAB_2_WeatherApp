namespace LAB_2.Models
{
    public class MyWeatherForecast
    {
        public string? City { get; private set; }
        public string? DataTimeText { get; private set; }
        public double? Temperature { get; private set; }
        public double? WindSpeed { get; private set; }
        public string? WeatherMain { get; private set; }
        public string? WeatherDescription { get; private set; }

        public MyWeatherForecast(string? city, string? dataTimeText, double? temperature,
            double? windSpeed, string? weatherMain, string? weatherDescription)
        {
            City = city;
            DataTimeText = dataTimeText;
            Temperature = temperature;
            WindSpeed = windSpeed;
            WeatherMain = weatherMain;
            WeatherDescription = weatherDescription;
        }

        public override string ToString()
        {
            return $"City: {City}; DataTime: {DataTimeText}; Temperature: {Temperature}; Wind speed: {WindSpeed}; Weather main: {WeatherMain}; Weather description: {WeatherDescription};";
        }
    }
}
