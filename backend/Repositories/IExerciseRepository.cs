using MyWorkoutApp.Models;

namespace MyWorkoutApp.Repositories
{
    public interface IExerciseRepository
    {
        Task<IEnumerable<Exercise>> GetAllExercisesAsync();
        Task<Exercise> CreateExerciseAsync(Exercise exercise);
        Task<Exercise?> GetByIdAsync(int id);
        Task<List<Models.SessionExercise>> GetExerciseSessionsAsync(int exerciseId, int userId);
        Task<PersonalRecord?> GetPersonalRecordAsync(int exerciseId, int userId);
        Task<List<PersonalRecord>> GetAllPersonalRecordsAsync(int userId);
    }
}
