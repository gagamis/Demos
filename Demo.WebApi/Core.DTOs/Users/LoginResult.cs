namespace Core.DTOs.Users
{
    public class LoginResult
    {
        public string TokenType { get; set; }
        public string Id { get; set; }
        public string RefreshToken { get; set; }
        public string AccessToken { get; set; }
        public int Expires { get; set; }
    }
}
