namespace CleanArchitecture.ModelContract.WebAPI.Response
{
    public class LoginResponse
    {
        public string? DisplayName { get; set; }
        public required string Token { get; set; }
        public required string Username { get; set; }
        public string? Image { get; set; }
        public required string Email { get; set; }
    }
}
