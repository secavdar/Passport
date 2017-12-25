using Passport.Domain.Type;

namespace Passport.Domain.ViewModel
{
    public class TokenInput
    {
        public GrantType GrantType { get; set; }
        public string Scope { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}