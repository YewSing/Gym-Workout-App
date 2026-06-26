using MyWorkoutApp.DTOs;
using MyWorkoutApp.Models;
using MyWorkoutApp.Repositories;

namespace MyWorkoutApp.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly IExerciseRepository _exerciseRepository;

        public ExerciseService(IExerciseRepository exerciseRepository)
        {
            _exerciseRepository = exerciseRepository;
        }

        public async Task<IEnumerable<ExerciseDto>> GetAllExercisesAsync(int userId)
        {
            var exercises = await _exerciseRepository.GetAllExercisesAsync();
            var prs = await _exerciseRepository.GetAllPersonalRecordsAsync(userId);
            var prMap = prs.ToDictionary(p => p.ExerciseId);

            return exercises.Select(e => new ExerciseDto
            {
                Id = e.Id,
                Name = e.Name,
                MuscleGroup = e.MuscleGroup,
                Description = e.Description,
                PhotoUrl = e.PhotoUrl,
                Pr = prMap.TryGetValue(e.Id, out var pr) ? new PRDto
                {
                    BestWeight = pr.BestWeight,
                    BestReps = pr.BestReps,
                    AchivedDate = pr.AchivedDate,
                } : null,
            });
        }

        public async Task<ExerciseDto> CreateExerciseAsync(CreateExerciseDto dto)
        {
            var exercise = new Exercise
            {
                Name = dto.Name,
                MuscleGroup = dto.MuscleGroup,
                Description = dto.Description,
                PhotoUrl = dto.PhotoUrl,
            };
            var created = await _exerciseRepository.CreateExerciseAsync(exercise);
            return new ExerciseDto
            {
                Id = created.Id,
                Name = created.Name,
                MuscleGroup = created.MuscleGroup,
                Description = created.Description,
                PhotoUrl = created.PhotoUrl,
            };
        }

        public async Task<ExerciseHistoryDto?> GetExerciseHistoryAsync(int exerciseId, int userId)
        {
            var exercise = await _exerciseRepository.GetByIdAsync(exerciseId);
            if (exercise == null) return null;

            var pr = await _exerciseRepository.GetPersonalRecordAsync(exerciseId, userId);
            var sessions = await _exerciseRepository.GetExerciseSessionsAsync(exerciseId, userId);

            return new ExerciseHistoryDto
            {
                ExerciseId = exercise.Id,
                Name = exercise.Name,
                MuscleGroup = exercise.MuscleGroup,
                Description = exercise.Description,
                PhotoUrl = exercise.PhotoUrl,
                Pr = pr == null ? null : new PRDto
                {
                    BestWeight = pr.BestWeight,
                    BestReps = pr.BestReps,
                    AchivedDate = pr.AchivedDate,
                },
                Sessions = sessions.Select(se => new ExerciseSessionDto
                {
                    DateTime = se.Session.DateTime,
                    MaxWeight = se.Sets.Count > 0 ? se.Sets.Max(s => s.Weight) : 0,
                    Volume = se.Sets.Sum(s => s.Weight * s.Reps),
                    Sets = se.Sets.Select(s => new SetSummaryDto
                    {
                        Weight = s.Weight,
                        Reps = s.Reps,
                    }).ToList(),
                }).ToList(),
            };
        }
    }
}
