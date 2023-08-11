using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Mvc;

using System;

using System.Linq;

using System.Threading.Tasks;

using Microsoft.Azure.Cosmos;



namespace WebApplication1.Controllers

{

    [ApiController]

    [Route("[controller]")]

    public class QuestionController : ControllerBase

    {

        private readonly string CosmosDBAccountConectKey = "AccountEndpoint=https://instance-clau.documents.azure.com:443/;AccountKey=UlvfGTFFGdQIwH6kh1RafmOvCP51Bjmj7Rt6efZVBffkM0CPwv7xxV3qTxQ8Cl5IpUJhazqaBBYlACDbqqbH3Q==;";
        private readonly string CosmosDbName = "Brainspark";
        private readonly string CosmosDbContainerName = "Questions";



        [HttpGet]

        public async Task<IActionResult> GetAllQuestions()
        {
            try
            {
                CosmosClient cosmosDbClient = new CosmosClient(CosmosDBAccountConectKey);
                Container containerClient = cosmosDbClient.GetContainer(CosmosDbName, CosmosDbContainerName);
                var query = new QueryDefinition("SELECT * FROM c");
                var iterator = containerClient.GetItemQueryIterator<Question>(query);
                var result = await iterator.ReadNextAsync();
                var questions = result.ToList();
                if (questions.Count > 0)
                {
                    return Ok(questions);
                }
                else
                {
                    return NotFound("No questions found.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to retrieve questions: " + ex.Message);
            }

        }



        [HttpGet("{questionId}")]

        public async Task<IActionResult> GetQuestionById(string questionId)

        {
            try
            {
                CosmosClient cosmosDbClient = new CosmosClient(CosmosDBAccountConectKey);
                Container containerClient = cosmosDbClient.GetContainer(CosmosDbName, CosmosDbContainerName);
                var query = new QueryDefinition("SELECT * FROM c WHERE c.id = @questionId")
                    .WithParameter("@questionId", questionId);
                var iterator = containerClient.GetItemQueryIterator<Question>(query);
                var result = await iterator.ReadNextAsync();
                var question = result.FirstOrDefault();
                if (question != null)
                {
                    return Ok(question);
                }
                else
                {
                    return NotFound("Question not found.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to retrieve question by ID: " + ex.Message);
            }
        }



        [HttpPut("{questionId}")]

        public async Task<IActionResult> UpdateQuestion(string questionId, [FromBody] Question updatedQuestion)

        {
            try
            {

                CosmosClient cosmosDbClient = new CosmosClient(CosmosDBAccountConectKey);
                Container containerClient = cosmosDbClient.GetContainer(CosmosDbName, CosmosDbContainerName);

                var response = await containerClient.ReadItemAsync<Question>(questionId, new PartitionKey(questionId));
                var existingQuestion = response.Resource;
                if (existingQuestion == null)
                {
                    return NotFound("Question not found.");
                }
                // Update properties of the existing question with the properties from updatedQuestion
                var replaceResponse = await containerClient.ReplaceItemAsync(existingQuestion, existingQuestion.id, new PartitionKey(questionId));
                return Ok(replaceResponse.Resource);
            }
            catch (CosmosException ex)
            {
                return BadRequest("Failed to update question: " + ex.Message);
            }
        }



        [HttpPost]

        public async Task<IActionResult> CreateQuestion([FromBody] Question newQuestion)
        {
            try
            {
                CosmosClient cosmosDbClient = new CosmosClient(CosmosDBAccountConectKey);
                Container containerClient = cosmosDbClient.GetContainer(CosmosDbName, CosmosDbContainerName);
                ItemResponse<Question> response = await containerClient.CreateItemAsync(newQuestion);
                return CreatedAtAction(nameof(GetQuestionById), new { questionId = newQuestion.id }, newQuestion);
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to create question: " + ex.Message);
            }
        }



        [HttpDelete("{questionId}")]

        public async Task<IActionResult> DeleteQuestion(string questionId)
        {
            try
            {
                CosmosClient cosmosDbClient = new CosmosClient(CosmosDBAccountConectKey);
                Container containerClient = cosmosDbClient.GetContainer(CosmosDbName, CosmosDbContainerName);
                ItemResponse<Question> response = await containerClient.DeleteItemAsync<Question>(questionId, new PartitionKey(questionId));
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return NoContent();
                }
                else
                {
                    return StatusCode((int)response.StatusCode, "Failed to delete question.");
                }
            }
            catch (CosmosException ex)
            {
               return BadRequest("Failed to delete question: " + ex.Message);
            }
        }
    }
}
