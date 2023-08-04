namespace MyCosmosApi.Models
{
    public class WeatherForecast
    {
        public string Id { get; set; }
        public string City { get; set; }
        public int TemperatureC { get; set; }
        public string Summary { get; set; }
        public DateOnly Date { get; set; }
    }
}

