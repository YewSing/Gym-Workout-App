using MyWorkoutApp.DTOs;

namespace MyWorkoutApp.Services
{
    public interface ISessionService
    {
        Task<int> CreateSessionAsync(CreateSessionDto dto);
        Task<int> AddExerciseAsync(int sessionId, AddExerciseToSessionDto dto);
        Task AddSetAsync(int sessionExerciseId, AddSetDto dto);
        Task<SessionDetailsDto> GetSessionDetailsAsync(int sessionId);
        Task<bool> FinishSessionAsync(int sessionId, int userId, FinishSessionDto dto);
        Task<LastSessionDto?> GetLastSessionForVariationAsync(int variationId);
    }

}
