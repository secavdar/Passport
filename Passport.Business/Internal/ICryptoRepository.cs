namespace Passport.Business.Internal
{
    public interface ICryptoRepository
    {
        string Hash(string input);
    }
}