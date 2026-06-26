namespace MyWorkoutApp.DTOs
{
    public class ExerciseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MuscleGroup { get; set; }
        public string? Description { get; set; }
        public string? PhotoUrl { get; set; }
        public PRDto? Pr { get; set; }
    }
}
