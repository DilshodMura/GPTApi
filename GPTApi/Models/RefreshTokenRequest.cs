using System.ComponentModel.DataAnnotations;

namespace GPTApi.Models
{
    public class RefreshTokenRequest
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}
