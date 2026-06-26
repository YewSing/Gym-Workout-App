using Microsoft.EntityFrameworkCore;
using MyWorkoutApp.Data;
using MyWorkoutApp.DTOs;
using MyWorkoutApp.Models;
using MyWorkoutApp.Repositories;

namespace MyWorkoutApp.Services
{
    public class WorkoutService : IWorkoutService
    {
        private readonly IWorkoutRepository _repo;
        private readonly AppDbContext _context; // To look up Exercise entities

        public WorkoutService(IWorkoutRepository repo, AppDbContext context)
        {
            _repo = repo;
            _context = context;
        }

        public async Task<WorkoutDto> AddWorkoutAsync(int userId, CreateWorkoutDto dto)
        {
            // Fetch the existing exercises to link them to the first variation
            var selectedExercises = await _context.Exercises
                .Where(e => dto.ExerciseIds.Contains(e.Id))
                .ToListAsync();

            var workout = new Workout
            {
                Name = dto.Name,
                Description = dto.Description,
                UserId = userId,
                Variations = new List<WorkoutVariation>
                {
                    new WorkoutVariation
                    {
                        Name = string.IsNullOrWhiteSpace(dto.VariationName) ? "Main" : dto.VariationName.Trim(),
                        VariationExercises = BuildOrderedVariationExercises(selectedExercises, dto.ExerciseIds)
                    }
                }
            };

            var savedWorkout = await _repo.CreateWorkoutAsync(workout);
            return MapWorkout(savedWorkout);
        }

        public async Task<IEnumerable<WorkoutDto>> GetUserWorkoutsAsync(int userId)
        {
            var workouts = await _repo.GetWorkoutsByUserIdAsync(userId);
            return workouts.Select(MapWorkout);
        }

        public async Task<WorkoutDto?> GetWorkoutByIdAsync(int id, int userId)
        {
            var workout = await _repo.GetWorkoutByIdAsync(id);
            if (workout == null || workout.UserId != userId) return null;
            return MapWorkout(workout);
        }

        public async Task<bool> UpdateWorkoutAsync(int id, int userId, CreateWorkoutDto dto)
        {
            // Plan-level update: name and description only. Exercises live on variations now.
            var workout = await _repo.GetWorkoutByIdAsync(id);
            if (workout == null || workout.UserId != userId) return false;

            workout.Name = dto.Name;
            workout.Description = dto.Description;

            await _repo.UpdateWorkoutAsync(workout);
            return true;
        }

        public async Task<bool> SoftDeleteWorkoutAsync(int id, int userId)
        {
            var workout = await _repo.GetWorkoutByIdAsync(id);
            if (workout == null || workout.UserId != userId) return false;

            workout.IsArchived = true;
            await _repo.UpdateWorkoutAsync(workout);
            return true;
        }

        // ── Variations ("Gyms") ──

        public async Task<VariationDto?> AddVariationAsync(int workoutId, int userId, CreateVariationDto dto)
        {
            var workout = await _repo.GetWorkoutByIdAsync(workoutId);
            if (workout == null || workout.UserId != userId) return null;

            var selectedExercises = await _context.Exercises
                .Where(e => dto.ExerciseIds.Contains(e.Id))
                .ToListAsync();

            var variation = new WorkoutVariation
            {
                WorkoutId = workoutId,
                Name = string.IsNullOrWhiteSpace(dto.Name) ? "Gym" : dto.Name.Trim(),
                VariationExercises = BuildOrderedVariationExercises(selectedExercises, dto.ExerciseIds)
            };

            var saved = await _repo.CreateVariationAsync(variation);
            return MapVariation(saved);
        }

        public async Task<bool> UpdateVariationAsync(int variationId, int userId, CreateVariationDto dto)
        {
            var variation = await _repo.GetVariationByIdAsync(variationId);
            if (variation == null || variation.Workout == null || variation.Workout.UserId != userId) return false;

            variation.Name = string.IsNullOrWhiteSpace(dto.Name) ? variation.Name : dto.Name.Trim();

            var selectedExercises = await _context.Exercises
                .Where(e => dto.ExerciseIds.Contains(e.Id))
                .ToListAsync();

            variation.VariationExercises.Clear();
            foreach (var ve in BuildOrderedVariationExercises(selectedExercises, dto.ExerciseIds))
                variation.VariationExercises.Add(ve);

            await _repo.UpdateVariationAsync(variation);
            return true;
        }

        public async Task<bool> DeleteVariationAsync(int variationId, int userId)
        {
            var variation = await _repo.GetVariationByIdAsync(variationId);
            if (variation == null || variation.Workout == null || variation.Workout.UserId != userId) return false;

            // A plan must always keep at least one gym.
            var count = await _repo.CountVariationsAsync(variation.WorkoutId);
            if (count <= 1) return false;

            await _repo.DeleteVariationAsync(variation);
            return true;
        }

        // ── Mapping helpers ──

        // Builds join rows in the order ids were sent, not the order the DB happens
        // to return them in (EF's Contains(...) query gives no order guarantee).
        private static List<VariationExercise> BuildOrderedVariationExercises(List<Exercise> exercises, List<int> orderedIds)
        {
            var byId = exercises.ToDictionary(e => e.Id);
            var result = new List<VariationExercise>();
            foreach (var id in orderedIds)
                if (byId.TryGetValue(id, out var ex))
                    result.Add(new VariationExercise { Exercise = ex });
            return result;
        }

        private static WorkoutDto MapWorkout(Workout w) => new WorkoutDto
        {
            Id = w.Id,
            Name = w.Name,
            Description = w.Description,
            Variations = (w.Variations ?? new List<WorkoutVariation>())
                .Select(MapVariation)
                .ToList()
        };

        private static VariationDto MapVariation(WorkoutVariation v) => new VariationDto
        {
            Id = v.Id,
            Name = v.Name,
            Exercises = (v.VariationExercises ?? new List<VariationExercise>())
                .OrderBy(ve => ve.Id)
                .Select(ve => new ExerciseDto
                {
                    Id = ve.Exercise.Id,
                    Name = ve.Exercise.Name,
                    MuscleGroup = ve.Exercise.MuscleGroup
                }).ToList(),
            Sessions = (v.Sessions ?? new List<Session>()).Select(s => new SessionSummaryDto
            {
                SessionId = s.SessionId,
                DateTime = s.DateTime,
                Duration = s.Duration,
                Volume = s.Volume
            }).OrderByDescending(s => s.DateTime).ToList()
        };
    }
}
