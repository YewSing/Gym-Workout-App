namespace MyWorkoutApp.DTOs
{
    public class ExerciseHistoryDto
    {
        public int ExerciseId { get; set; }
        public string Name { get; set; }
        public string MuscleGroup { get; set; }
        public string? Description { get; set; }
        public string? PhotoUrl { get; set; }
        public PRDto? Pr { get; set; }
        public List<ExerciseSessionDto> Sessions { get; set; } = new();
    }

    public class PRDto
    {
        public double BestWeight { get; set; }
        public int BestReps { get; set; }
        public DateTime AchivedDate { get; set; }
    }

    public class ExerciseSessionDto
    {
        public DateTime DateTime { get; set; }
        public double MaxWeight { get; set; }
        public double Volume { get; set; }
        public List<SetSummaryDto> Sets { get; set; } = new();
    }

    public class SetSummaryDto
    {
        public double Weight { get; set; }
        public int Reps { get; set; }
    }
}
