using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWorkoutApp.DTOs;
using MyWorkoutApp.Services;

namespace MyWorkoutApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ExercisesController : ControllerBase
    {
        private readonly IExerciseService _exerciseService;

        public ExercisesController(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return Unauthorized();

            var exercises = await _exerciseService.GetAllExercisesAsync(int.Parse(userIdClaim.Value));
            return Ok(exercises);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateExerciseDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name) || string.IsNullOrWhiteSpace(dto.MuscleGroup))
                return BadRequest("Name and MuscleGroup are required.");

            var result = await _exerciseService.CreateExerciseAsync(dto);
            return Ok(result);
        }

        [HttpGet("{id}/history")]
        public async Task<IActionResult> GetHistory(int id)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return Unauthorized();

            var result = await _exerciseService.GetExerciseHistoryAsync(id, int.Parse(userIdClaim.Value));
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}
