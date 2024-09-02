namespace CleanArchitecture.ModelContract.WebAPI.Response
{
    public class LoginResponse
    {
        public string DisplayName { get; set; }
        public string Token { get; set; }
        public string Username { get; set; }
        public string Image { get; set; }
        public string Email { get; set; }
    }
}
