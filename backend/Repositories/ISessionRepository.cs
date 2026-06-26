using MyWorkoutApp.Models;

namespace MyWorkoutApp.Repositories
{
    public interface ISessionRepository
    {
        Task<Session> CreateSessionAsync(Session session);
        Task<Session> GetSessionByIdAsync(int id);
        Task<Session?> GetLastSessionForVariationAsync(int variationId);
        Task AddExerciseAsync(SessionExercise exercise);
        Task AddSetAsync(Set set);
        Task<List<PersonalRecord>> GetPersonalRecordsAsync(IEnumerable<int> exerciseIds, int userId);
        Task AddPersonalRecordAsync(PersonalRecord pr);
        Task SaveChangesAsync();
    }

}
