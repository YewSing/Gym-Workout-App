namespace MyWorkoutApp.Models
{
    public class Set
    {
        public int SetId { get; set; }
        public int SessionExerciseId { get; set; }
        public int Reps { get; set; }
        public double Weight { get; set; }
        public TimeSpan BreakTime { get; set; }
        public SessionExercise SessionExercise { get; set; }
    }
}
