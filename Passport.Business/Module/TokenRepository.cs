using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Passport.Business.Contract;
using Passport.Business.Extension;
using Passport.Domain.Model;
using Passport.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

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
        }

        public TokenResponse Generate(TokenRequest tokenRequest)
        {
            var clients = _serviceProvider.GetService<IEnumerable<Client>>();
            var client = clients.IsMatch(tokenRequest);

            if (client == null)
                throw new InvalidOperationException("Client is not valid");

            var jwt = new JwtSecurityToken(
                issuer: _httpContextAccessor.HttpContext.Request.Host.Value,
                audience: _httpContextAccessor.HttpContext.Request.Host.Value,
                notBefore: _options.NotBefore,
                expires: _options.Expiration,
                signingCredentials: _options.SigningCredentials);

            jwt.Payload.Add("clientId", client.Id);
            jwt.Payload.Add("scope", client.AllowedScopes);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new TokenResponse
            {
                AccessToken = encodedJwt,
                ExpiersIn = (int)_options.ValidFor.TotalSeconds,
                TokenType = "Bearer"
            };
        }
        public ValidateResponse Validate(ValidateToken validateToken)
        {
            var validationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = _options.SigningCredentials.Key,
                ValidateAudience = false,
                ValidateIssuer = true,
                ValidIssuer = validateToken.Issuer
            };

            try
            {
                new JwtSecurityTokenHandler().ValidateToken(validateToken.AccessToken, validationParameters, out var securityToken);
                var jwt = (JwtSecurityToken)securityToken;

                return new ValidateResponse
                {
                    Claims = jwt.Claims
                                .Where(x => x.Type != "scope")
                                .ToDictionary(x => x.Type, x => x.Value),
                    AllowedScopes = jwt.Claims
                                       .Where(x => x.Type == "scope")
                                       .Select(x => x.Value)
                                       .ToList()
                };
            }
            catch
            {
                return null;
            }
        }
    }
}