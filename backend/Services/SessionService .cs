using MyWorkoutApp.DTOs;
using MyWorkoutApp.Models;
using MyWorkoutApp.Repositories;

namespace MyWorkoutApp.Services
{
    public class SessionService : ISessionService
    {
        private readonly ISessionRepository _repo;
        private readonly ILogger<SessionService> _logger;

        public SessionService(ISessionRepository repo, ILogger<SessionService> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<int> CreateSessionAsync(CreateSessionDto dto)
        {
            var session = new Session
            {
                WorkoutVariationId = dto.WorkoutVariationId,
                // PostgreSQL 'timestamp with time zone' requires UTC; normalize whatever the client sent.
                DateTime = dto.DateTime.ToUniversalTime(),
                Duration = TimeSpan.Zero
            };

            await _repo.CreateSessionAsync(session);
            return session.SessionId;
        }

        public async Task<int> AddExerciseAsync(int sessionId, AddExerciseToSessionDto dto)
        {
            var exercise = new SessionExercise
            {
                SessionId = sessionId,
                ExerciseId = dto.ExerciseId,
                Note = dto.Note
            };

            await _repo.AddExerciseAsync(exercise);
            await _repo.SaveChangesAsync();
            return exercise.SessionExerciseId;
        }

        public async Task AddSetAsync(int sessionExerciseId, AddSetDto dto)
        {
            var set = new Set
            {
                SessionExerciseId = sessionExerciseId,
                Reps = dto.Reps,
                Weight = dto.Weight,
                BreakTime = dto.BreakTime
            };

            await _repo.AddSetAsync(set);
            await _repo.SaveChangesAsync();
        }

        public async Task<SessionDetailsDto> GetSessionDetailsAsync(int sessionId)
        {
            var session = await _repo.GetSessionByIdAsync(sessionId);

            return new SessionDetailsDto
            {
                SessionId = session.SessionId,
                DateTime = session.DateTime,
                Duration = session.Duration,
                TotalVolume = session.SessionExercises.SelectMany(e => e.Sets).Sum(s => s.Reps * s.Weight),
                Exercises = session.SessionExercises.Select(se => new ExerciseInSessionDto
                {
                    ExerciseId = se.ExerciseId,
                    Name = se.Exercise.Name,
                    Note = se.Note,
                    Sets = se.Sets.Select(s => new SetDto
                    {
                        Reps = s.Reps,
                        Weight = s.Weight,
                        BreakTime = s.BreakTime
                    }).ToList()
                }).ToList()
            };
        }

        public async Task<bool> FinishSessionAsync(int sessionId, int userId, FinishSessionDto dto)
        {
            var session = await _repo.GetSessionByIdAsync(sessionId);
            if (session == null) return false;

            if (dto.Debug != null)
            {
                _logger.LogWarning(
                    "FinishSession debug: sessionId={SessionId} duration={Duration} startTime={StartTime} clientNow={ClientNow} restoreSource={RestoreSource} restoreDetail={RestoreDetail}",
                    sessionId, dto.Duration, dto.Debug.StartTime, dto.Debug.ClientNow, dto.Debug.RestoreSource, dto.Debug.RestoreDetail);
            }

            session.Duration = dto.Duration;

            var exerciseIds = session.SessionExercises.Select(se => se.ExerciseId).Distinct().ToList();
            var existingPrs = await _repo.GetPersonalRecordsAsync(exerciseIds, userId);
            var prMap = existingPrs.ToDictionary(p => p.ExerciseId);

            foreach (var se in session.SessionExercises)
            {
                if (!se.Sets.Any()) continue;

                var bestSet = se.Sets.MaxBy(s => s.Weight);
                if (bestSet == null) continue;

                if (prMap.TryGetValue(se.ExerciseId, out var existing))
                {
                    if (bestSet.Weight > existing.BestWeight)
                    {
                        existing.BestWeight = bestSet.Weight;
                        existing.BestReps = bestSet.Reps;
                        existing.AchivedDate = session.DateTime;
                    }
                }
                else
                {
                    await _repo.AddPersonalRecordAsync(new PersonalRecord
                    {
                        UserId = userId,
                        ExerciseId = se.ExerciseId,
                        BestWeight = bestSet.Weight,
                        BestReps = bestSet.Reps,
                        AchivedDate = session.DateTime,
                    });
                }
            }

            await _repo.SaveChangesAsync();
            return true;
        }

        public async Task<LastSessionDto?> GetLastSessionForVariationAsync(int variationId)
        {
            var session = await _repo.GetLastSessionForVariationAsync(variationId);
            if (session == null) return null;

            return new LastSessionDto
            {
                SessionId = session.SessionId,
                DateTime = session.DateTime,
                Exercises = (session.SessionExercises ?? new List<SessionExercise>()).Select(se => new LastExerciseDto
                {
                    ExerciseId = se.ExerciseId,
                    Sets = (se.Sets ?? new List<Set>()).Select(s => new LastSetDto
                    {
                        Reps = s.Reps,
                        Weight = s.Weight
                    }).ToList()
                }).ToList()
            };
        }
    }

}
