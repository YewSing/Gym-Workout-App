namespace MyWorkoutApp.DTOs
{
    public class CreateVariationDto
    {
        public string Name { get; set; } = string.Empty;
        public List<int> ExerciseIds { get; set; } = new List<int>();
    }
}
