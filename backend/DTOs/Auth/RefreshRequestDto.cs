using System.ComponentModel.DataAnnotations;

namespace MyWorkoutApp.DTOs.Auth
{
    public class RefreshRequestDto
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}
