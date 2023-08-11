using BrainSpark.Client.Shared;

namespace BrainSpark.Client.Services
{
	public class ExamService : IExamService
	{
		private readonly HttpClient _http;
        public ExamService(HttpClient http)
        {
			_http = http;
		}
		public List<ExamCategory> ExamCategories { get; set; } = new List<ExamCategory>();

		public async Task GetExamCategories()
		{
			var result = await _http.GetFromJsonAsync<ServiceResponse<List<ExamCategory>>>("api/exam");
			if (result != null && result.Data != null)
			{
				ExamCategories = result.Data;
			}
		}

		public async Task<List<ExamCategory>> GetExamCategoriesByType(string categoryType)
		{
			try
			{
				var result = await _http.GetFromJsonAsync<ServiceResponse<List<ExamCategory>>>($"api/exam/categoryType={categoryType}");
				return result?.Data;
			}
			catch (Exception ex)
			{
				// Handle exceptions appropriately
				throw new Exception("Failed to fetch exam categories.", ex);
			}
		}
	}
}