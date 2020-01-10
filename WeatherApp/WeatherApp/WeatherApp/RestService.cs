using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WeatherApp
{
    public class RestService
    {
        HttpClient _client;

        public RestService()
        {
            _client = new HttpClient();
        }

        public async Task<WeatherData> GetWeatherData(string query)
        {
            WeatherData weatherData = null;
            try
            {
                var response = await _client.GetAsync(query)
                    .ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync()
                        .ConfigureAwait(false);
                    weatherData = JsonConvert.DeserializeObject<WeatherData>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\t\terror {0}", ex.Message);
            }

            return weatherData;
        }

        public async Task<ForecastData> GetForecastData(string query)
        {
            ForecastData forecastData = null;
            try
            {
                var response = await _client.GetAsync(query)
                        .ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync()
                        .ConfigureAwait(false);
                    forecastData = JsonConvert.DeserializeObject<ForecastData>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\t\terror {0}", ex.Message);
            }
            return forecastData;
        }
    }
}
