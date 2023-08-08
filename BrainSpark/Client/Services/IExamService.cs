namespace BrainSpark.Client.Services
{
	public interface IExamService
	{
		List<ExamCategory> ExamCategories { get; set; }
		Task GetExamCategories();
	}
}
