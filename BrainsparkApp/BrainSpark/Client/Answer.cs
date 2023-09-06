namespace BrainSpark.Client
{
	public class Answer
	{
		public string? id { get; set; }
		public string? answerId { get; set; }
		public string? userId { get; set; }
		public string? questionId { get; set; }
		public string Text { get; set; } = string.Empty;
		public string? SelectedOption { get; set; }
		public DateTime Timestamp { get; set; }
		public bool IsCorrect { get; set; }
	}
}
