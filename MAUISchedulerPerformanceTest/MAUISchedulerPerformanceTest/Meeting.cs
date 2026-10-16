namespace MAUISchedulerPerformanceTest
{
    public class Meeting
    {
        public int Id { get; set; }

        public string Subject { get; set; } = string.Empty;

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Notes { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public bool IsAllDay { get; set; }

        public Brush? BackgroundColor { get; set; }
    }
}
