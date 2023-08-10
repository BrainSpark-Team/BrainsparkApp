namespace BrainSpark.Client
{
	public class Question
	{
		public string Title { set; get; } = string.Empty;
		public List<string>? Options { set; get; }
		public string CorrectOption { set; get; } = string.Empty;
	}
}
