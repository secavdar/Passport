using Passport.Domain.ViewModel;

namespace Passport.Business.Contract
{
    public interface ITokenRepository
    {
        TokenResponse Generate(TokenInput tokenInput);
    }
}