namespace MyWorkoutApp.DTOs
{
    // Temporary fields for tracking down a bug where Duration is saved as 0 despite
    // a real elapsed time on the client. Safe to remove once that's root-caused.
    public class FinishSessionDebugDto
    {
        public long? StartTime { get; set; }
        public long? ClientNow { get; set; }
        public string? RestoreSource { get; set; }
        public string? RestoreDetail { get; set; }
    }

    public class FinishSessionDto
    {
        public TimeSpan Duration { get; set; }
        public FinishSessionDebugDto? Debug { get; set; }
    }
}
