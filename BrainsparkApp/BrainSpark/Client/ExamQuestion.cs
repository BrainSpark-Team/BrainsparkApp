namespace BrainSpark.Client
{
	public class ExamQuestion{
		public string? id { get; set; }
		public string? questionId { get; set; }
		public string? Text { get; set; }
		public string? Type { get; set; }
		public string? examId { get; set; }
		public string? examinerId { get; set; }
		public string? userId { get; set; }
		public List<Answer> Answers { get; set; } 
	}
}
