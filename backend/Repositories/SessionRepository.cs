using Microsoft.EntityFrameworkCore;
using MyWorkoutApp.Data;
using MyWorkoutApp.Models;

namespace MyWorkoutApp.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly AppDbContext _db;

        public SessionRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Session> CreateSessionAsync(Session session)
        {
            await _db.Sessions.AddAsync(session);
            await _db.SaveChangesAsync();
            return session;
        }

        public async Task<Session> GetSessionByIdAsync(int id)
        {
            return await _db.Sessions
                .Include(s => s.SessionExercises)
                .ThenInclude(se => se.Sets)
                .Include(s => s.SessionExercises)
                .ThenInclude(se => se.Exercise)
                .FirstOrDefaultAsync(s => s.SessionId == id);
        }

        public async Task<Session?> GetLastSessionForVariationAsync(int variationId)
        {
            return await _db.Sessions
                .Where(s => s.WorkoutVariationId == variationId)
                .OrderByDescending(s => s.DateTime)
                .Include(s => s.SessionExercises)
                    .ThenInclude(se => se.Sets)
                .FirstOrDefaultAsync();
        }

        public async Task AddExerciseAsync(SessionExercise exercise)
        {
            await _db.SessionExercises.AddAsync(exercise);
        }

        public async Task AddSetAsync(Set set)
        {
            await _db.Sets.AddAsync(set);
        }

        public async Task<List<PersonalRecord>> GetPersonalRecordsAsync(IEnumerable<int> exerciseIds, int userId)
        {
            return await _db.PersonalRecords
                .Where(p => p.UserId == userId && exerciseIds.Contains(p.ExerciseId))
                .ToListAsync();
        }

        public async Task AddPersonalRecordAsync(PersonalRecord pr)
        {
            await _db.PersonalRecords.AddAsync(pr);
        }

        public async Task SaveChangesAsync() => await _db.SaveChangesAsync();
    }

}
