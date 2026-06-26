namespace MyWorkoutApp.Models
{
    public class User
    {
        public int UserId { get; set; }

        // Auth
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        // Profile
        public string UserName { get; set; } = string.Empty;

        // Metadata
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public List<Workout> Workouts { get; set; } = new List<Workout>();
    }
}
