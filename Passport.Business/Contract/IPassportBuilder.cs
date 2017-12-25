using Microsoft.Extensions.DependencyInjection;

namespace Passport.Business.Contract
{
    public interface IPassportBuilder
    {
        IServiceCollection Services { get; }
    }
}