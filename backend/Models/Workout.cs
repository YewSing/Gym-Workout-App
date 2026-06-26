namespace MyWorkoutApp.Models
{
    public class Workout
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }

        // A plan always has one or more variations ("Gyms"). Each variation holds
        // its own exercise list and its own sessions.
        public ICollection<WorkoutVariation> Variations { get; set; } = new List<WorkoutVariation>();

        // For Soft Delete
        public bool IsArchived { get; set; } = false;
    }
}
