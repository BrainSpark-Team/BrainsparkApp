namespace BrainSpark.Client
{
	public class Test
	{
		public int TestId { get; set; }
		public string Title { get; set; } = string.Empty;
		public List<Question>? Questions { get; set; }
		public string TestCategory { get; set; } = string.Empty;
	}
}
