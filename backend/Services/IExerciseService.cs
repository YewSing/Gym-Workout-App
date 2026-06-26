using MyWorkoutApp.DTOs;

namespace MyWorkoutApp.Services
{
    public interface IExerciseService
    {
        Task<IEnumerable<ExerciseDto>> GetAllExercisesAsync(int userId);
        Task<ExerciseDto> CreateExerciseAsync(CreateExerciseDto dto);
        Task<ExerciseHistoryDto?> GetExerciseHistoryAsync(int exerciseId, int userId);
    }
}
