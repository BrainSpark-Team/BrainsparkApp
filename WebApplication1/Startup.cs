using Microsoft.Extensions.Configuration;
using Microsoft.Azure.Cosmos;
using System.Configuration;

namespace WebApplication1
{
    public class Startup
    {
// ...

public void ConfigureServices(IServiceCollection services)
    {

            services.AddSingleton((serviceProvider) =>
            {
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                var cosmosDBAccountConectKey = configuration.GetConnectionString("CosmosDBConnection");
                return new CosmosClient(cosmosDBAccountConectKey);
            });

        }

}
}
