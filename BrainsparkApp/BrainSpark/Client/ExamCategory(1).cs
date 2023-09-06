namespace BrainSpark.Client
{
	public class ExamCategory
	{
		public int examId { get; set; }
		public string CategoryType { get; set; } = string.Empty;
		public string SubCategoryType { get; set; } = string.Empty;
		public string ImgUrl { get; set; } = string.Empty;
		public string ImgAlt { get; set; } = string.Empty;
		public string TimeLimit { get; set; } = string.Empty;
		public string Title { get; set; } = string.Empty;
		public int TotalQuestions { get; set; }
		public int Questions { get; set; }
		public string Difficulty { get; set; } = string.Empty;
		public string LearningResources { get; set; } = string.Empty;
		public DateTime AvailabilityStart { get; set; }
		public DateTime AvailabilityEnd { get; set; }
		public TimeOnly TestDuration { get; set; }
		public bool IsPubliclyAvailable { get; set; }
		public int? MaxAttempts { get; set; }
	}
}