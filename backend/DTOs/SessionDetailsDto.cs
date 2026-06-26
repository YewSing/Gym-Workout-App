namespace MyWorkoutApp.DTOs
{
    public class SessionDetailsDto
    {
        public int SessionId { get; set; }
        public DateTime DateTime { get; set; }
        public TimeSpan Duration { get; set; }
        public double TotalVolume { get; set; }

        public List<ExerciseInSessionDto> Exercises { get; set; }
    }

    public class ExerciseInSessionDto
    {
        public int ExerciseId { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public List<SetDto> Sets { get; set; }
    }

    public class SetDto
    {
        public int Reps { get; set; }
        public double Weight { get; set; }
        public TimeSpan BreakTime { get; set; }
        public double Volume => Reps * Weight;
    }

}
