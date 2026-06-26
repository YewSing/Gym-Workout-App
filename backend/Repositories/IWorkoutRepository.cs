using MyWorkoutApp.Models;

namespace MyWorkoutApp.Repositories
{
    public interface IWorkoutRepository
    {
        Task<Workout> CreateWorkoutAsync(Workout workout);
        Task<IEnumerable<Workout>> GetWorkoutsByUserIdAsync(int userId);
        Task<Workout?> GetWorkoutByIdAsync(int id);
        Task UpdateWorkoutAsync(Workout workout);

        // Variation ("Gym") operations
        Task<WorkoutVariation> CreateVariationAsync(WorkoutVariation variation);
        Task<WorkoutVariation?> GetVariationByIdAsync(int variationId);
        Task<int> CountVariationsAsync(int workoutId);
        Task UpdateVariationAsync(WorkoutVariation variation);
        Task DeleteVariationAsync(WorkoutVariation variation);
    }
}
