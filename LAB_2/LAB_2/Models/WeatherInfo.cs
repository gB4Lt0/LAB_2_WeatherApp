﻿namespace LAB_2.Models
{
    public class WeatherInfo
    {
        public int? dt { get; set; }
        public WeatherInfoMain? main { get; set; }
        public WeatherInfoWind? wind { get; set; }
        public IList<WeatherInfoWeatherItem>? weather { get; set; }
        public string? dt_txt { get; set; }
    }
}
