namespace MyWorkoutApp.DTOs
{
    public class SessionSummaryDto
    {
        public int SessionId { get; set; }
        public DateTime DateTime { get; set; }
        public TimeSpan Duration { get; set; }
        public double Volume { get; set; }
    }
}
