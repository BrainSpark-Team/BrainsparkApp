using BrainSpark.Client.Shared;
using System.Net.Http;

namespace BrainSpark.Client.Services
{
	public class ExamService : IExamService
	{
		private readonly HttpClient _httpClient;

		public ExamService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<ExamModel[]> GetExamsAsync()
		{
			return await _httpClient.GetFromJsonAsync<ExamModel[]>("https://wa-brainspark-pol-dev.azurewebsites.net/exams");
		}
	}
}