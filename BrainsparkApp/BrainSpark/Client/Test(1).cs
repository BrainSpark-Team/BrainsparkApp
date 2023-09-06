namespace BrainSpark.Client
{
	public class Test
	{
		public int TestId { get; set; }
		public string Title { get; set; } = string.Empty;
		public List<Question>? Questions { get; set; }
		public string TestCategory { get; set; } = string.Empty;
		public string TestSubCategory { get; set; }
		public TimeOnly TestDuration { get; set; }
		public string LearningResources { get; set; }
		public DateTime AvailabilityStart { get; set; }
		public DateTime AvailabilityEnd { get; set; }
		public bool IsPubliclyAvailable { get; set; }
	}
}
