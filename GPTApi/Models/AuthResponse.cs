namespace GPTApi.Models
{
    public class AuthResponse
    {
        public long UserId { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
