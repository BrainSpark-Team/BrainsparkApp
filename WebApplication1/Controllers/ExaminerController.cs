using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class ExaminerController : ControllerBase
    {
        
        private readonly string CosmosDBAccountConectKey = "AccountEndpoint=https://instance-clau.documents.azure.com:443/;AccountKey=UlvfGTFFGdQIwH6kh1RafmOvCP51Bjmj7Rt6efZVBffkM0CPwv7xxV3qTxQ8Cl5IpUJhazqaBBYlACDbqqbH3Q==;";
        private readonly string CosmosDbName = "Brainspark";
        private readonly string CosmosDbContainerName = "Examiners";

        [HttpGet]
        public async Task<IActionResult> GetAllExaminerDetails()
        {
            try
            {
       
                CosmosClient cosmosDbClient = new CosmosClient(CosmosDBAccountConectKey);
                Container containerClient = cosmosDbClient.GetContainer(CosmosDbName, CosmosDbContainerName);

            
                var query = new QueryDefinition("SELECT * FROM c");

                var iterator = containerClient.GetItemQueryIterator<Examiner>(query);
                var result = await iterator.ReadNextAsync();

                var examiner = result.ToList(); 

                if (examiner.Count > 0)
                {
                    return Ok(examiner);
                }
                else
                {
                    return NotFound("No examiner found.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to retrieve examiner details: " + ex.Message);
            }
        }

        [HttpGet("{examinerId}")]
        public async Task<IActionResult> GetExaminerDetailsById(string examinerId)
        {
            try
            {
                CosmosClient cosmosDbClient = new CosmosClient(CosmosDBAccountConectKey);
                Container containerClient = cosmosDbClient.GetContainer(CosmosDbName, CosmosDbContainerName);

                var query = new QueryDefinition("SELECT * FROM c WHERE c.id = @examinerId")
                    .WithParameter("@examinerId", examinerId);

                var iterator = containerClient.GetItemQueryIterator<Examiner>(query);
                var result = await iterator.ReadNextAsync();

                var examiner = result.FirstOrDefault(); 

                if (examiner != null)
                {
                    return Ok(examiner);
                }
                else
                {
                    return NotFound("Examiner not found.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to retrieve examiner details by ID: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateExaminer([FromBody] Examiner newExaminer)
        {
            try
            {
                if (string.IsNullOrEmpty(newExaminer.examinerId))
                {
                    newExaminer.examinerId = Guid.NewGuid().ToString(); 
                }

                CosmosClient cosmosDbClient = new CosmosClient(CosmosDBAccountConectKey);
                Container containerClient = cosmosDbClient.GetContainer(CosmosDbName, CosmosDbContainerName);

                ItemResponse<Examiner> response = await containerClient.CreateItemAsync(newExaminer);

                return CreatedAtAction(nameof(GetExaminerDetailsById), new { ExaminerId = newExaminer.examinerId }, newExaminer);
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to create user: " + ex.Message);
            }
        }




        [HttpPut("{examinerId}")]
        public async Task<IActionResult> UpdateExaminer(string examinerId, [FromBody] Examiner updatedExaminer)
        {
            try
            {
                CosmosClient cosmosDbClient = new CosmosClient(CosmosDBAccountConectKey);
                Container containerClient = cosmosDbClient.GetContainer(CosmosDbName, CosmosDbContainerName);
                var response = await containerClient.ReadItemAsync<Examiner>(examinerId, new PartitionKey(examinerId));

                var existingExaminer = response.Resource;

                if (existingExaminer == null)
                {
                    return NotFound("Examiner not found.");
                }

                
                existingExaminer.UserName = updatedExaminer.UserName;
                existingExaminer.FirstName = updatedExaminer.FirstName;
                existingExaminer.LastName = updatedExaminer.LastName;
                existingExaminer.Email = updatedExaminer.Email;
                existingExaminer.Password = updatedExaminer.Password;
                existingExaminer.Country = updatedExaminer.Country;

                updatedExaminer.examinerId = existingExaminer.examinerId;

                var replaceResponse = await containerClient.ReplaceItemAsync(existingExaminer, existingExaminer.examinerId, new PartitionKey(existingExaminer.examinerId));

                return Ok(replaceResponse.Resource);
            }
            catch (CosmosException ex)
            {
                return BadRequest("Failed to update examiner: " + ex.Message);
            }
        }


        [HttpDelete("{examinerId}")]
        public async Task<IActionResult> DeleteExaminer(string examinerId)
        {
            try
            {
                CosmosClient cosmosDbClient = new CosmosClient(CosmosDBAccountConectKey);
                Container containerClient = cosmosDbClient.GetContainer(CosmosDbName, CosmosDbContainerName);

                ItemResponse<Examiner> response = await containerClient.DeleteItemAsync<Examiner>(examinerId, new PartitionKey(examinerId));

                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return NoContent();
                }
                else
                {
                    return StatusCode((int)response.StatusCode, "Failed to delete examiner.");
                }
            }
            catch (CosmosException ex)
            {
                return BadRequest("Failed to delete examiner: " + ex.Message);
            }
        }


    }
}
