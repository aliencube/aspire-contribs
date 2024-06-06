using Aspire.Contribs.WebApp.Models;

namespace Aspire.Contribs.WebApp.Clients;

public interface IApiClient
{
    Task<IEnumerable<WeatherForecast>> GetWeatherForecastAsync();
}

public class ApiClient(HttpClient http) : IApiClient
{
    private readonly HttpClient _http = http ?? throw new ArgumentNullException(nameof(http));

    public async Task<IEnumerable<WeatherForecast>> GetWeatherForecastAsync()
    {
        var forecasts = await _http.GetFromJsonAsync<IEnumerable<WeatherForecast>>("/weatherforecast");

        return forecasts ?? [];
    }
}
