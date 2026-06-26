using Microsoft.EntityFrameworkCore;
using MyWorkoutApp.Data;
using MyWorkoutApp.Models;

namespace MyWorkoutApp.Repositories
{
    public class ExerciseRepository : IExerciseRepository
    {
        private readonly AppDbContext _context;

        public ExerciseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Exercise>> GetAllExercisesAsync()
        {
            return await _context.Exercises.ToListAsync();
        }

        public async Task<Exercise> CreateExerciseAsync(Exercise exercise)
        {
            _context.Exercises.Add(exercise);
            await _context.SaveChangesAsync();
            return exercise;
        }

        public async Task<Exercise?> GetByIdAsync(int id)
        {
            return await _context.Exercises.FindAsync(id);
        }

        public async Task<List<SessionExercise>> GetExerciseSessionsAsync(int exerciseId, int userId)
        {
            return await _context.SessionExercises
                .Include(se => se.Sets)
                .Include(se => se.Session)
                    .ThenInclude(s => s.WorkoutVariation)
                        .ThenInclude(wv => wv.Workout)
                .Where(se => se.ExerciseId == exerciseId
                             && se.Session.WorkoutVariation.Workout.UserId == userId)
                .OrderByDescending(se => se.Session.DateTime)
                .Take(20)
                .ToListAsync();
        }

        public async Task<PersonalRecord?> GetPersonalRecordAsync(int exerciseId, int userId)
        {
            return await _context.PersonalRecords
                .FirstOrDefaultAsync(p => p.ExerciseId == exerciseId && p.UserId == userId);
        }

        public async Task<List<PersonalRecord>> GetAllPersonalRecordsAsync(int userId)
        {
            return await _context.PersonalRecords
                .Where(p => p.UserId == userId)
                .ToListAsync();
        }
    }
}
