namespace MyWorkoutApp.Models
{
    // A "Gym" in the UI: the same Workout (plan) performed at a particular place
    // with its own equipment/exercise list and its own logged sessions.
    public class WorkoutVariation
    {
        public int Id { get; set; }

        public int WorkoutId { get; set; }
        public Workout? Workout { get; set; }

        // The gym name, e.g. "Apartment Gym", "School Gym".
        public string Name { get; set; } = string.Empty;

        // This gym's exercise list, in order. Join entity so insertion order survives reloads.
        public ICollection<VariationExercise> VariationExercises { get; set; } = new List<VariationExercise>();

        // Sessions logged for this gym.
        public ICollection<Session> Sessions { get; set; } = new List<Session>();
    }
}
