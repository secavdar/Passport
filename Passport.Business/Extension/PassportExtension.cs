using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Passport.Business.Contract;
using Passport.Business.Service;

namespace Passport.Business.Extension
{
    public static class PassportExtension
    {
        public static IApplicationBuilder UsePassportServer(this IApplicationBuilder app)
        {
            var tokenRepository = app.ApplicationServices.GetService<ITokenRepository>();

            app.Map("/token/generate", x =>
            {
                x.Run(async context =>
                {
                    var service = new TokenService(tokenRepository);
                    await service.Generate(context);
                });
            });

            app.Map("/token/validate", x =>
            {
                x.Run(async context =>
                {
                    var service = new TokenService(tokenRepository);
                    await service.Validate(context);
                });
            });

            return app;
        }
    }
}