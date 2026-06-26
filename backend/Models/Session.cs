namespace MyWorkoutApp.Models
{
    public class Session
    {
        public int SessionId { get; set; }
        public int WorkoutVariationId { get; set; }
        public DateTime DateTime { get; set; }
        public TimeSpan Duration { get; set; }
        public WorkoutVariation WorkoutVariation { get; set; }
        public List<SessionExercise> SessionExercises { get; set; }
        public double Volume =>
            SessionExercises?.Sum(se =>
                se.Sets.Sum(s => s.Reps * s.Weight)
            ) ?? 0;

    }
}
