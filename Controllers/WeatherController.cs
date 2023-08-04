using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using MyCosmosApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCosmosApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly CosmosClient cosmosClient;
        private readonly Container container;

        public WeatherController(IConfiguration configuration)
        {
            string endpointUri = "https://lborlea19.documents.azure.com:443/";
            string primaryKey = "CpPG1Wef8PZt1ecCI2AGD2w5LStVoQJmcNsb6vIAKgPN31wqEOozVmZTW8D7eCRz3QmVb438OQmXACDbSjYLvw==";
            string databaseId = "data_base";
            string containerId = "container1";

            cosmosClient = new CosmosClient(endpointUri, primaryKey);
            var database = cosmosClient.GetDatabase(databaseId);
            container = database.GetContainer(containerId);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWeatherForecasts()
        {
            var query = "SELECT * FROM c";
            var resultSetIterator = container.GetItemQueryIterator<WeatherForecast>(query);

            List<WeatherForecast> forecasts = new List<WeatherForecast>();
            while (resultSetIterator.HasMoreResults)
            {
                FeedResponse<WeatherForecast> response = await resultSetIterator.ReadNextAsync();
                forecasts.AddRange(response.ToList());
            }

            return Ok(forecasts);
        }

        [HttpPost]
        public async Task<IActionResult> AddWeatherForecast(WeatherForecast forecast)
        {
            forecast.Id = Guid.NewGuid().ToString();
            var response = await container.CreateItemAsync(forecast, new PartitionKey(forecast.City));
            return Ok(response.Resource);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWeatherForecast(string id, WeatherForecast updatedForecast)
        {
            var existingForecast = await container.ReadItemAsync<WeatherForecast>(id, new PartitionKey(updatedForecast.City));
            if (existingForecast.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return NotFound();
            }

            updatedForecast.Id = existingForecast.Resource.Id;
            updatedForecast.City = existingForecast.Resource.City;
            var response = await container.ReplaceItemAsync(updatedForecast, updatedForecast.Id, new PartitionKey(updatedForecast.City));
            return Ok(response.Resource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWeatherForecast(string id, string city)
        {
            var existingForecast = await container.ReadItemAsync<WeatherForecast>(id, new PartitionKey(city));
            if (existingForecast.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return NotFound();
            }

            var response = await container.DeleteItemAsync<WeatherForecast>(id, new PartitionKey(city));
            return Ok(response.Resource); 
             
        }
    }
}
