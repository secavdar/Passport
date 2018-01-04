using Passport.Domain.ViewModel;

namespace Passport.Business.Contract
{
    public interface ITokenRepository
    {
        TokenResponse Generate(TokenRequest tokenInput);
        ValidateResponse Validate(ValidateToken validateToken);
    }
}