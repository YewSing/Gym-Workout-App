namespace MyWorkoutApp.Models
{
    // Explicit join row (instead of an implicit many-to-many) so the order
    // exercises were added in is preserved via its own auto-increment Id —
    // the same mechanism SessionExercise already relies on for ordering.
    public class VariationExercise
    {
        public int Id { get; set; }
        public int WorkoutVariationId { get; set; }
        public WorkoutVariation WorkoutVariation { get; set; }
        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; }
    }
}
