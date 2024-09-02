using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.ModelContract.WebAPI.Request
{
    public class RegisterRequest
    {
        [Required]
        public string DisplayName { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{6,8}$", ErrorMessage = "密碼最少為6碼，應包含大小寫、數字")]
        public string Password { get; set; }
    }
}
