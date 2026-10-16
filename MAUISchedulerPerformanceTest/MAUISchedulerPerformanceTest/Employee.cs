namespace MAUISchedulerPerformanceTest
{
    public class Employee
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int GroupId { get; set; }

        public Brush? BackgroundColor { get; set; }

        public Brush? ForegroundColor { get; set; }

        public bool IsNodeCollapsed { get; set; }
    }
}
