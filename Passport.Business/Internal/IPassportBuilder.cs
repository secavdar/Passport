using Microsoft.Extensions.DependencyInjection;

namespace Passport.Business.Internal
{
    public interface IPassportBuilder
    {
        IServiceCollection Services { get; }
    }
}