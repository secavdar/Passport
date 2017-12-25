﻿using Microsoft.AspNetCore.Builder;
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

            app.Map("/connect/token", x =>
            {
                x.Run(async context =>
                {
                    var service = new ConnectService(tokenRepository);
                    await service.GetResponse(context);
                });
            });

            return app;
        }
    }
}