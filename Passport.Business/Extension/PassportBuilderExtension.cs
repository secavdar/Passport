using Passport.Business.Contract;
using Passport.Business.Module;
using Passport.Domain.Model;
using System.Collections.Generic;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class PassportBuilderExtension
    {
        public static IPassportBuilder AddPassportServer(this IServiceCollection services)
        {
            services.AddTransient<ITokenRepository, TokenRepository>();
            services.AddTransient<ICryptoRepository, CryptoRepository>();

            return new PassportBuilder(services);
        }

        public static IPassportBuilder AddInMemoryClients(this IPassportBuilder builder, IEnumerable<Client> clients)
        {
            builder.Services.AddSingleton(clients);
            return builder;
        }
    }
}