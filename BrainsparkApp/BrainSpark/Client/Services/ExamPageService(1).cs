namespace BrainSpark.Client.Services
{
	public class ExamPageService
	{
		public string ExamTitle { get; set; } = "Untitled Exam";
		public List<ExamQuestion> ExamQuestions { get; } = new List<ExamQuestion>();

		public void AddQuestion(ExamQuestion question)
		{
			ExamQuestions.Add(question);
		}
	}
}
