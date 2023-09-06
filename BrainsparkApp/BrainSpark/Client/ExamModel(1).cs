namespace BrainSpark.Client
{
    public class ExamModel
    {
        public string Id { get; set; }
        public string ExamId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string ExamType { get; set; }
        public DateTime AvailabilityStart { get; set; }
        public DateTime AvailabilityEnd { get; set; }
        public DateTime PublicityDate { get; set; }
        public bool IsPublic { get; set; }
        public int MaxAttemptDuration { get; set; }
    }

}
