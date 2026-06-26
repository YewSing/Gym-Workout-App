namespace MyWorkoutApp.Models
{
    public class PersonalRecord
    {
        public int PersonalRecordId { get; set; }
        public int UserId { get; set; }
        public int ExerciseId { get; set; }
        public int BestReps { get; set; }
        public double BestWeight { get; set; }
        public DateTime AchivedDate { get; set; }

        public User User { get; set; }
        public Exercise Exercise { get; set; }
    }
}
