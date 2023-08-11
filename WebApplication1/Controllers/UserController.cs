using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using Microsoft.Azure.Cosmos;

namespace WebApplication1.Controllers

{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly string CosmosDBAccountConectKey = "AccountEndpoint=https://instance-clau.documents.azure.com:443/;AccountKey=UlvfGTFFGdQIwH6kh1RafmOvCP51Bjmj7Rt6efZVBffkM0CPwv7xxV3qTxQ8Cl5IpUJhazqaBBYlACDbqqbH3Q==;";
        private readonly string CosmosDbName = "Brainspark";
        private readonly string CosmosDbContainerName = "Users";

        [HttpGet]
        public async Task<IActionResult> GetAllUserDetails()
        {
            try
            {
                CosmosClient cosmosDbClient = new CosmosClient(CosmosDBAccountConectKey);
                Microsoft.Azure.Cosmos.Container containerClient = cosmosDbClient.GetContainer(CosmosDbName, CosmosDbContainerName);

                var query = new QueryDefinition("SELECT * FROM c");

                var iterator = containerClient.GetItemQueryIterator<User>(query);
                var result = await iterator.ReadNextAsync();

                var users = result.ToList(); 

                if (users.Count > 0)
                {
                    return Ok(users);
                }
                else
                {
                    return NotFound("No users found.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to retrieve user details: " + ex.Message);
            }
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserDetailsById(string userId)
        {
            try
            {
               
                CosmosClient cosmosDbClient = new CosmosClient(CosmosDBAccountConectKey);
                Microsoft.Azure.Cosmos.Container containerClient = cosmosDbClient.GetContainer(CosmosDbName, CosmosDbContainerName);

                var query = new QueryDefinition("SELECT * FROM c WHERE c.id = @userId")
                    .WithParameter("@userId", userId);

                var iterator = containerClient.GetItemQueryIterator<User>(query);
                var result = await iterator.ReadNextAsync();

                var user = result.FirstOrDefault(); 

                if (user != null)
                {
                    return Ok(user);
                }
                else
                {
                    return NotFound("User not found.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to retrieve user details by ID: " + ex.Message);
            }
        }



        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(string userId, [FromBody] User updatedUser)
        {
            try
            {
                CosmosClient cosmosDbClient = new CosmosClient(CosmosDBAccountConectKey);
                Microsoft.Azure.Cosmos.Container containerClient = cosmosDbClient.GetContainer(CosmosDbName, CosmosDbContainerName);

                var response = await containerClient.ReadItemAsync<User>(userId, new PartitionKey(userId));

                var existingUser = response.Resource;

                if (existingUser == null)
                {
                    return NotFound("User not found.");
                }

                existingUser.UserName = updatedUser.UserName;
                existingUser.FirstName = updatedUser.FirstName;
                existingUser.LastName = updatedUser.LastName;
                existingUser.Email = updatedUser.Email;
                existingUser.Password = updatedUser.Password;
                existingUser.Country = updatedUser.Country;

                updatedUser.id = existingUser.id;

                var replaceResponse = await containerClient.ReplaceItemAsync(existingUser, existingUser.id, new PartitionKey(userId));

                return Ok(replaceResponse.Resource);
            }
            catch (CosmosException ex)
            {
                return BadRequest("Failed to update user: " + ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User newUser)
        {
            try
            {

                CosmosClient cosmosDbClient = new CosmosClient(CosmosDBAccountConectKey);
                Microsoft.Azure.Cosmos.Container containerClient = cosmosDbClient.GetContainer(CosmosDbName, CosmosDbContainerName);

                ItemResponse<User> response = await containerClient.CreateItemAsync(newUser);

                return CreatedAtAction(nameof(GetUserDetailsById), new { userId = newUser.id }, newUser);
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to create user: " + ex.Message);
            }
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            try
            {
                CosmosClient cosmosDbClient = new CosmosClient(CosmosDBAccountConectKey);
                Microsoft.Azure.Cosmos.Container containerClient = cosmosDbClient.GetContainer(CosmosDbName, CosmosDbContainerName);

                ItemResponse<User> response = await containerClient.DeleteItemAsync<User>(userId, new PartitionKey(userId));

                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return NoContent();
                }
                else
                {
                    return StatusCode((int)response.StatusCode, "Failed to delete user.");
                }
            }
            catch (CosmosException ex)
            {
                return BadRequest("Failed to delete user: " + ex.Message);
            }
        }

    }
}

