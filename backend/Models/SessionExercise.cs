namespace MyWorkoutApp.Models
{
    public class SessionExercise
    {
        public int SessionExerciseId { get; set; }
        public int SessionId { get; set; }
        public int ExerciseId { get; set; }
        public string Note { get; set; }
        public List<Set> Sets { get; set; }
        public Session Session {  get; set; }
        public Exercise Exercise {  get; set; }

    }
}
