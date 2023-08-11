namespace BrainSpark.Client
{
	public class Exams
	{
		public string Title { get; set; } = string.Empty;
		public List<ExamQuestion> ExamQuestions = new List<ExamQuestion>();
	}
}
