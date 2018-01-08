using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Passport.Business.Contract;
using Passport.Business.Internal;
using Passport.Business.Module;
using Passport.Domain;
using Passport.Domain.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class PassportBuilderExtension
    {
        private static IServiceCollection AddDefaultTransient<TService, TImplementation>(this IServiceCollection services) where TService : class where TImplementation : class, TService
        {
            if (!services.Any(x => x.ServiceType == typeof(TService)))
                services.AddTransient<TService, TImplementation>();
            return services;
        }

        public static IPassportBuilder AddPassportServer(this IServiceCollection services)
        {
            services.AddDefaultTransient<ITokenRepository, TokenRepository>()
                    .AddDefaultTransient<ICryptoRepository, Sha512CryptoRepository>();

            return new PassportBuilder(services);
        }

        public static IPassportBuilder AddSecretKey(this IPassportBuilder builder, string secretKey)
        {
            builder.Services.AddOptions();

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

            builder.Services.Configure<PassportOptions>(options =>
            {
                options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            });

            return builder;
        }

        public static IPassportBuilder AddInMemoryClients(this IPassportBuilder builder, IEnumerable<ClientVm> clients)
        {
            builder.Services.AddDefaultTransient<IClientRepository, ClientMemoryRepository>()
                            .AddDefaultTransient<IClientScopeRepository, ClientScopeRepository>()
                            .AddDefaultTransient<IClientSecretRepository, ClientSecretRepository>()
                            .AddDefaultTransient<IScopeRepository, ScopeRepository>();

            builder.Services.AddSingleton(clients);
            return builder;
        }

        public static IPassportBuilder AddInMemoryScopes(this IPassportBuilder builder, IEnumerable<ScopeVm> scopes)
        {
            builder.Services.AddSingleton(scopes);
            return builder;
        }

        public static IPassportBuilder AddDbContext<TContext>(this IPassportBuilder builder, string connectionString) where TContext : DbContext
        {
            builder.Services.AddDefaultTransient<IClientRepository, ClientRepository>()
                            .AddDefaultTransient<IClientScopeRepository, ClientScopeRepository>()
                            .AddDefaultTransient<IClientSecretRepository, ClientSecretRepository>()
                            .AddDefaultTransient<IScopeRepository, ScopeRepository>();

            builder.Services.AddDbContext<TContext>(x => x.UseSqlServer(connectionString));
            return builder;
        }
    }
}