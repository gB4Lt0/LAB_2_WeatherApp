using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LAB_2.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LAB_2
{
    public class DataBaseManager
    {
        private const string ConnectionString = "Data Source=database.db; Version = 3; New = True; Compress = True;";

        private readonly SQLiteConnection _sqLiteConnection;

        public DataBaseManager()
        {
            _sqLiteConnection = new SQLiteConnection(ConnectionString);
            try
            {
                _sqLiteConnection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void CreateTableIfNotExist()
        {
            SQLiteCommand sqLiteCommand = _sqLiteConnection.CreateCommand();
            sqLiteCommand.CommandText = "CREATE TABLE IF NOT EXISTS WeatherForecast (" +
                                        "city TEXT, " +
                                        "dt_text VARCHAR(20), " +
                                        "temp NUMERIC, " +
                                        "wind_speed NUMERIC, " +
                                        "weather_main TEXT, " +
                                        "weather_description TEXT" +
                                        ")";
            sqLiteCommand.ExecuteNonQuery();
        }

        public void AddData(MyWeatherForecast wfs)
        {
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append("INSERT INTO WeatherForecast ")
                        .Append("(city, dt_text, temp, wind_speed, weather_main, weather_description) ")
                        .Append("VALUES(@city, @dtText, @temp, @windSpeed, @weatherMain, @weatherDescription)");

            using (SQLiteCommand command = new SQLiteCommand(queryBuilder.ToString(), _sqLiteConnection))
            {
                command.Parameters.AddWithValue("@city", wfs.City);
                command.Parameters.AddWithValue("@dtText", wfs.DataTimeText);
                command.Parameters.AddWithValue("@temp", wfs.Temperature);
                command.Parameters.AddWithValue("@windSpeed", wfs.WindSpeed);
                command.Parameters.AddWithValue("@weatherMain", wfs.WeatherMain);
                command.Parameters.AddWithValue("@weatherDescription", wfs.WeatherDescription);
                command.ExecuteNonQuery();
            }
        }

        public List<MyWeatherForecast> GetData()
        {
            SQLiteCommand sqLiteCommand = _sqLiteConnection.CreateCommand();
            sqLiteCommand.CommandText = "SELECT * FROM WeatherForecast";
            SQLiteDataReader sqLiteDataReader = sqLiteCommand.ExecuteReader();
            List<MyWeatherForecast> data = new List<MyWeatherForecast>();
            while (sqLiteDataReader.Read())
            {
                data.Add(new MyWeatherForecast(
                    sqLiteDataReader.GetString(0),
                    sqLiteDataReader.GetString(1),
                    sqLiteDataReader.GetDouble(2),
                    sqLiteDataReader.GetDouble(3),
                    sqLiteDataReader.GetString(4),
                    sqLiteDataReader.GetString(5)
                    ));
            }

            return data;
        }
    }
}
