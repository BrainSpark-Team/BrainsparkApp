namespace BrainSpark.Client
{
	public class ExamQuestion{
		public int Id { get; set; }
		public string Text { get; set; } = string.Empty;
		public List<Answer> Answers { get; set; } 
	}
}
