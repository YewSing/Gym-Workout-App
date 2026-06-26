using MyWorkoutApp.DTOs;
using MyWorkoutApp.Models;

namespace MyWorkoutApp.Services
{
    public interface IWorkoutService
    {
        Task<WorkoutDto> AddWorkoutAsync(int userId, CreateWorkoutDto dto);
        Task<IEnumerable<WorkoutDto>> GetUserWorkoutsAsync(int userId);
        Task<WorkoutDto?> GetWorkoutByIdAsync(int id, int userId);
        Task<bool> UpdateWorkoutAsync(int id, int userId, CreateWorkoutDto dto);
        Task<bool> SoftDeleteWorkoutAsync(int id, int userId);

        // Variation ("Gym") operations
        Task<VariationDto?> AddVariationAsync(int workoutId, int userId, CreateVariationDto dto);
        Task<bool> UpdateVariationAsync(int variationId, int userId, CreateVariationDto dto);
        Task<bool> DeleteVariationAsync(int variationId, int userId);
    }
}
