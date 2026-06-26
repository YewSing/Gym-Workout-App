using Microsoft.EntityFrameworkCore;
using MyWorkoutApp.Data;
using MyWorkoutApp.Models;

namespace MyWorkoutApp.Repositories
{
    public class WorkoutRepository : IWorkoutRepository
    {
        private readonly AppDbContext _context;
        public WorkoutRepository(AppDbContext context) => _context = context;

        public async Task<Workout> CreateWorkoutAsync(Workout workout)
        {
            _context.Workouts.Add(workout);
            await _context.SaveChangesAsync();
            return workout;
        }

        public async Task<IEnumerable<Workout>> GetWorkoutsByUserIdAsync(int userId)
        {
            return await _context.Workouts
                .Where(w => w.UserId == userId && !w.IsArchived)
                .Include(w => w.Variations)
                    .ThenInclude(v => v.VariationExercises)
                        .ThenInclude(ve => ve.Exercise)
                .Include(w => w.Variations)
                    .ThenInclude(v => v.Sessions)
                        .ThenInclude(s => s.SessionExercises)
                            .ThenInclude(se => se.Sets)
                .ToListAsync();
        }

        public async Task<Workout?> GetWorkoutByIdAsync(int id)
        {
            return await _context.Workouts
                .Include(w => w.Variations)
                    .ThenInclude(v => v.VariationExercises)
                        .ThenInclude(ve => ve.Exercise)
                .Include(w => w.Variations)
                    .ThenInclude(v => v.Sessions)
                        .ThenInclude(s => s.SessionExercises)
                            .ThenInclude(se => se.Sets)
                .FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task UpdateWorkoutAsync(Workout workout)
        {
            _context.Workouts.Update(workout);
            await _context.SaveChangesAsync();
        }

        // ── Variations ("Gyms") ──

        public async Task<WorkoutVariation> CreateVariationAsync(WorkoutVariation variation)
        {
            _context.WorkoutVariations.Add(variation);
            await _context.SaveChangesAsync();
            return variation;
        }

        public async Task<WorkoutVariation?> GetVariationByIdAsync(int variationId)
        {
            return await _context.WorkoutVariations
                .Include(v => v.Workout)
                .Include(v => v.VariationExercises)
                    .ThenInclude(ve => ve.Exercise)
                .Include(v => v.Sessions)
                    .ThenInclude(s => s.SessionExercises)
                        .ThenInclude(se => se.Sets)
                .FirstOrDefaultAsync(v => v.Id == variationId);
        }

        public async Task<int> CountVariationsAsync(int workoutId)
        {
            return await _context.WorkoutVariations.CountAsync(v => v.WorkoutId == workoutId);
        }

        public async Task UpdateVariationAsync(WorkoutVariation variation)
        {
            _context.WorkoutVariations.Update(variation);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteVariationAsync(WorkoutVariation variation)
        {
            _context.WorkoutVariations.Remove(variation);
            await _context.SaveChangesAsync();
        }
    }
}
