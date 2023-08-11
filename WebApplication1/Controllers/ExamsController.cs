using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Mvc;

using Microsoft.Azure.Cosmos;

using System;

using System.Collections.Generic;

using System.Linq;

using System.Threading.Tasks;



namespace WebApplication1.Controllers

{

    [ApiController]

    [Route("[controller]")]

    public class ExamsController : ControllerBase

    {

        private readonly string CosmosDBAccountConectKey = "AccountEndpoint=https://instance-clau.documents.azure.com:443/;AccountKey=UlvfGTFFGdQIwH6kh1RafmOvCP51Bjmj7Rt6efZVBffkM0CPwv7xxV3qTxQ8Cl5IpUJhazqaBBYlACDbqqbH3Q==;";

        private readonly string CosmosDbName = "Brainspark";

        private readonly string CosmosDbContainerName = "Exams";



        [HttpGet]
        public async Task<IActionResult> GetAllExams()

        {
            try
            {
                CosmosClient cosmosDbClient = new CosmosClient(CosmosDBAccountConectKey);
                Container containerClient = cosmosDbClient.GetContainer(CosmosDbName, CosmosDbContainerName);

                var query = new QueryDefinition("SELECT * FROM c");
                var iterator = containerClient.GetItemQueryIterator<Exam>(query);
                var result = await iterator.ReadNextAsync();
                var exams = result.ToList();
                if (exams.Count > 0)
                {
                    return Ok(exams);
                }
                else
                {
                    return NotFound("No exams found.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to retrieve exams: " + ex.Message);
            }
        }



        [HttpGet("{examId}")]

        public async Task<IActionResult> GetExamById(string examId)

        {
            try
            {
                CosmosClient cosmosDbClient = new CosmosClient(CosmosDBAccountConectKey);
                Container containerClient = cosmosDbClient.GetContainer(CosmosDbName, CosmosDbContainerName);
                var query = new QueryDefinition("SELECT * FROM c WHERE c.id = @examId")

                    .WithParameter("@examId", examId);
                var iterator = containerClient.GetItemQueryIterator<Exam>(query);
                var result = await iterator.ReadNextAsync();
                var exam = result.FirstOrDefault();
                if (exam != null)
                {
                    return Ok(exam);
                }

                else

                {
                    return NotFound("Exam not found.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to retrieve exam: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateExam([FromBody] Exam newExam)
        {
            try
            {
                if (string.IsNullOrEmpty(newExam.examId))
                {
                    newExam.examId = Guid.NewGuid().ToString();
                }

                CosmosClient cosmosDbClient = new CosmosClient(CosmosDBAccountConectKey);
                Container containerClient = cosmosDbClient.GetContainer(CosmosDbName, CosmosDbContainerName);

                ItemResponse<Exam> response = await containerClient.CreateItemAsync(newExam);

                return CreatedAtAction(nameof(GetExamById), new { ExamId = newExam.examId }, newExam);
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to create user: " + ex.Message);
            }
        }

        [HttpPut("{examId}")]

        public async Task<IActionResult> UpdateExam(string examId, [FromBody] Exam updatedExam)

        {
            try

            {

                CosmosClient cosmosDbClient = new CosmosClient(CosmosDBAccountConectKey);

                Container containerClient = cosmosDbClient.GetContainer(CosmosDbName, CosmosDbContainerName);

                var response = await containerClient.ReadItemAsync<Exam>(examId, new PartitionKey(examId));

                var existingExam = response.Resource;



                if (existingExam == null)

                {

                    return NotFound("Exam not found.");

                }
                existingExam.Title = updatedExam.Title;

                existingExam.StartTime = updatedExam.StartTime;

                existingExam.EndTime = updatedExam.EndTime;
                var replaceResponse = await containerClient.ReplaceItemAsync(existingExam, existingExam.examId, new PartitionKey(examId));



                return Ok(replaceResponse.Resource);

            }

            catch (CosmosException ex)

            {

                return BadRequest("Failed to update exam: " + ex.Message);

            }

        }

        [HttpDelete("{examId}")]
        public async Task<IActionResult> DeleteExam(string examId)

        {
            try

            {
                CosmosClient cosmosDbClient = new CosmosClient(CosmosDBAccountConectKey);

                Container containerClient = cosmosDbClient.GetContainer(CosmosDbName, CosmosDbContainerName);
                var response = await containerClient.DeleteItemAsync<Exam>(examId, new PartitionKey(examId));
                return NoContent();
            }
            catch (CosmosException ex)
            {
                return BadRequest("Failed to delete exam: " + ex.Message);
            }

        }

    }

}
