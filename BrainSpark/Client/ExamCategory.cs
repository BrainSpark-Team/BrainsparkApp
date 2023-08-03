namespace BrainSpark.Client
{
	public class ExamCategory
	{
		public string ImgUrl { get; set; } = string.Empty;
		public string ImgAlt { get; set; } = string.Empty;
		public string TimeLimit { get; set; } = string.Empty;
		public string Title { get; set; } = string.Empty;
		public int Questions { get; set; }
		public string Difficulty { get; set; } = string.Empty;
	}
}
