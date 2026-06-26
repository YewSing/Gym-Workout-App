using System.Collections.Generic;

namespace MyWorkoutApp.DTOs
{
    public class WorkoutDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        // Each variation is a "Gym": its own exercise list and session history.
        public List<VariationDto> Variations { get; set; } = new List<VariationDto>();
    }

    public class VariationDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<ExerciseDto> Exercises { get; set; } = new List<ExerciseDto>();
        public List<SessionSummaryDto> Sessions { get; set; } = new List<SessionSummaryDto>();
    }
}
