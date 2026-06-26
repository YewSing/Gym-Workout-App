using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWorkoutApp.DTOs;
using MyWorkoutApp.Services;

namespace MyWorkoutApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _service;

        public SessionController(ISessionService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSession(CreateSessionDto dto)
        {
            var id = await _service.CreateSessionAsync(dto);
            return Ok(id);
        }

        [HttpPost("{sessionId}/exercise")]
        public async Task<IActionResult> AddExercise(int sessionId, AddExerciseToSessionDto dto)
        {
            var id = await _service.AddExerciseAsync(sessionId, dto);
            return Ok(id);
        }

        [HttpPost("exercise/{sessionExerciseId}/set")]
        public async Task<IActionResult> AddSet(int sessionExerciseId, AddSetDto dto)
        {
            await _service.AddSetAsync(sessionExerciseId, dto);
            return Ok();
        }

        [HttpGet("{sessionId}")]
        public async Task<IActionResult> GetDetails(int sessionId)
        {
            var details = await _service.GetSessionDetailsAsync(sessionId);
            return Ok(details);
        }

        [HttpPut("{sessionId}")]
        public async Task<IActionResult> Finish(int sessionId, FinishSessionDto dto)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return Unauthorized();

            var success = await _service.FinishSessionAsync(sessionId, int.Parse(userIdClaim.Value), dto);
            if (!success) return NotFound();
            return NoContent();
        }

        // Most recent session for a gym, used to prefill "previous" weight/reps.
        [HttpGet("variation/{variationId}/last")]
        public async Task<IActionResult> GetLastForVariation(int variationId)
        {
            var last = await _service.GetLastSessionForVariationAsync(variationId);
            if (last == null) return NoContent();
            return Ok(last);
        }
    }

}
