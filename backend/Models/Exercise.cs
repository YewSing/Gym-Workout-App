namespace MyWorkoutApp.Models
{
    public class Exercise
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MuscleGroup { get; set; }
        public string? Description { get; set; }
        public string? PhotoUrl { get; set; }

        // Join rows linking this exercise to the workout variations ("Gyms") it belongs to.
        public ICollection<VariationExercise> VariationExercises { get; set; } = new List<VariationExercise>();

        public List<SessionExercise> SessionExercises { get; set; } = new List<SessionExercise>();
    }
}
