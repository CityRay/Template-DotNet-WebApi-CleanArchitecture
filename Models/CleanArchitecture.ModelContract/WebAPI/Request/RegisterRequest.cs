using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.ModelContract.WebAPI.Request
{
    public class RegisterRequest
    {
        [Required]
        public required string DisplayName { get; set; }
        [Required]
        public required string Username { get; set; }
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        [Required]
        [RegularExpression("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{6,8}$", ErrorMessage = "密碼最少為6碼，應包含大小寫、數字")]
        public required string Password { get; set; }
    }
}
