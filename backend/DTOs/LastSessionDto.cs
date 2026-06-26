namespace MyWorkoutApp.DTOs
{
    // The most recent session logged for a variation ("Gym"), used to prefill
    // the "previous" weight/reps when starting a new workout at the same gym.
    public class LastSessionDto
    {
        public int SessionId { get; set; }
        public DateTime DateTime { get; set; }
        public List<LastExerciseDto> Exercises { get; set; } = new List<LastExerciseDto>();
    }

    public class LastExerciseDto
    {
        public int ExerciseId { get; set; }
        public List<LastSetDto> Sets { get; set; } = new List<LastSetDto>();
    }

    public class LastSetDto
    {
        public int Reps { get; set; }
        public double Weight { get; set; }
    }
}
