using Microsoft.Extensions.DependencyInjection;
using Passport.Business.Contract;
using System;

namespace Passport.Business.Module
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