using Microsoft.Extensions.DependencyInjection;
using System;

namespace Passport.Business.Internal
{
    public class PassportBuilder : IPassportBuilder
    {
        public PassportBuilder(IServiceCollection services)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public IServiceCollection Services { get; }
    }
}