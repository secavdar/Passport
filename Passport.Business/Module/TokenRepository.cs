using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Passport.Business.Contract;
using Passport.Business.Extension;
using Passport.Domain.Model;
using Passport.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;

namespace Passport.Business.Module
{
    public class TokenRepository : ITokenRepository
    {
        private readonly PassportOptions _options;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IServiceProvider _serviceProvider;

        public TokenRepository(IOptions<PassportOptions> options, IHttpContextAccessor httpContextAccessor, IServiceProvider serviceProvider)
        {
            _httpContextAccessor = httpContextAccessor;
            _options = options.Value;
            _serviceProvider = serviceProvider;

            ThrowIfInvalidOptions(_options);
        }

        public TokenResponse Generate(TokenInput tokenInput)
        {
            var clients = _serviceProvider.GetService<IEnumerable<Client>>();
            var client = clients.IsMatch(tokenInput);

            if (client == null)
                throw new InvalidOperationException("Client is not valid");

            var jwt = new JwtSecurityToken(
                issuer: _httpContextAccessor.HttpContext.Request.Host.Value,
                audience: _httpContextAccessor.HttpContext.Request.Host.Value,
                notBefore: _options.NotBefore,
                expires: _options.Expiration,
                signingCredentials: _options.SigningCredentials);

            jwt.Payload.Add("client_id", client.Id);
            jwt.Payload.Add("scope", client.AllowedScopes);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new TokenResponse
            {
                AccessToken = encodedJwt,
                ExpiersIn = (int)_options.ValidFor.TotalSeconds,
                TokenType = "Bearer"
            };
        }

        private void ThrowIfInvalidOptions(PassportOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.ValidFor <= TimeSpan.Zero)
            {
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(PassportOptions.ValidFor));
            }

            if (options.SigningCredentials == null)
            {
                throw new ArgumentNullException(nameof(PassportOptions.SigningCredentials));
            }

            if (options.JtiGenerator == null)
            {
                throw new ArgumentNullException(nameof(PassportOptions.JtiGenerator));
            }
        }
    }
}