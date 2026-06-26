namespace MyWorkoutApp.DTOs
{
    public class CreateExerciseDto
    {
        public string Name { get; set; }
        public string MuscleGroup { get; set; }
        public string? Description { get; set; }
        public string? PhotoUrl { get; set; }
    }
}
