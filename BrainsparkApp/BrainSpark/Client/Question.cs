﻿namespace BrainSpark.Client
{
	public class Question
	{
		public string Questionn { get; set; } = string.Empty;
		public List<string>? Options { get; set; }
		public string CorrectOption { get; set; } = string.Empty;
	}
}
