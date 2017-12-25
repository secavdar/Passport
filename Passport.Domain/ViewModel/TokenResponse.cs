namespace Passport.Domain.ViewModel
{
    public class TokenResponse
    {
        public string AccessToken { get; set; }
        public int ExpiersIn { get; set; }
        public string TokenType { get; set; }
    }
}