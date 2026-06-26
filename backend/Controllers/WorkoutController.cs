using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWorkoutApp.DTOs;
using MyWorkoutApp.Services;

namespace MyWorkoutApp.Controllers
{
    [Authorize] // Requires the JWT token saved in your frontend
    [ApiController]
    [Route("api/[controller]")]
    public class WorkoutsController : ControllerBase
    {
        private readonly IWorkoutService _workoutService;

        public WorkoutsController(IWorkoutService workoutService)
        {
            _workoutService = workoutService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateWorkoutDto dto)
        {
            // Extract UserId from the JWT token claims
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return Unauthorized();

            var workout = await _workoutService.AddWorkoutAsync(int.Parse(userIdClaim.Value), dto);
            return Ok(workout);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return Unauthorized();

            var workouts = await _workoutService.GetUserWorkoutsAsync(int.Parse(userIdClaim.Value));
            return Ok(workouts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return Unauthorized();

            var workout = await _workoutService.GetWorkoutByIdAsync(id, int.Parse(userIdClaim.Value));
            if (workout == null) return NotFound();

            return Ok(workout);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateWorkoutDto dto)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return Unauthorized();

            var success = await _workoutService.UpdateWorkoutAsync(id, int.Parse(userIdClaim.Value), dto);
            if (!success) return NotFound();

            return NoContent(); // 204 No Content
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return Unauthorized();

            var success = await _workoutService.SoftDeleteWorkoutAsync(id, int.Parse(userIdClaim.Value));
            if (!success) return NotFound();

            return NoContent();
        }

        // ── Variations ("Gyms") ──

        [HttpPost("{workoutId}/variations")]
        public async Task<IActionResult> AddVariation(int workoutId, CreateVariationDto dto)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return Unauthorized();

            var variation = await _workoutService.AddVariationAsync(workoutId, int.Parse(userIdClaim.Value), dto);
            if (variation == null) return NotFound();

            return Ok(variation);
        }

        [HttpPut("variations/{variationId}")]
        public async Task<IActionResult> UpdateVariation(int variationId, CreateVariationDto dto)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return Unauthorized();

            var success = await _workoutService.UpdateVariationAsync(variationId, int.Parse(userIdClaim.Value), dto);
            if (!success) return NotFound();

            return NoContent();
        }

        [HttpDelete("variations/{variationId}")]
        public async Task<IActionResult> DeleteVariation(int variationId)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return Unauthorized();

            var success = await _workoutService.DeleteVariationAsync(variationId, int.Parse(userIdClaim.Value));
            // false here usually means "last remaining gym" or not found / not owned.
            if (!success) return BadRequest(new { message = "Cannot delete this gym (it may be the only one)." });

            return NoContent();
        }
    }
}
