namespace BrainSpark.Client.Services
{
	public interface IExamService
	{
		Task<ExamModel[]> GetExamsAsync();
	}
}
