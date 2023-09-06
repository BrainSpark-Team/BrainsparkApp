namespace BrainSpark.Client
{
	public class Exams
	{
		public string? id { get; set; }
		public string? examId { get; set; }
		public string Title { get; set; } = string.Empty;
		public string? Description { get; set; }
		public string? Category { get; set; }
		public string? ExamType { get; set; }
		public DateTime AvailabilityStart { get; set; }
		public DateTime AvailabilityEnd { get; set; }
		public DateTime PublicityDate { get; set; }
		public bool IsPublic { get; set; }
		public int MaxAttemptDuration { get; set; }
		public List<ExamQuestion> Questions = new List<ExamQuestion>();
		public List<ExamCategory> ExamSettings = new List<ExamCategory>();
    }
}
