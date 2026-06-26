namespace MyWorkoutApp.DTOs
{
    public class CreateWorkoutDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        // Name of the first variation ("Gym") created with the plan. Defaults to "Main" if empty.
        public string? VariationName { get; set; }

        public List<int> ExerciseIds { get; set; } // IDs of exercises for the first variation
    }
}
