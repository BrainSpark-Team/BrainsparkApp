namespace BrainSpark.Client
{
	public class ExamQuestions{
		public string ExamQuestion { get; set; } = String.Empty;
		public List<string>? Options { get; set; } 
		public int CorrectOption { get; set; }
	}
}
