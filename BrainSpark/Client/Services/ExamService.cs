
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
			var result = await _http.GetFromJsonAsync<List<ExamCategory>>("api/product");
			if (result != null)
			{
				ExamCategories = result;
			}
		}
	}
}

