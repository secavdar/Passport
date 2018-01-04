using Microsoft.AspNetCore.Http;
using Passport.Business.Contract;
using Passport.Business.Service.Base;
using Passport.Domain.ViewModel;
using System;
using System.Threading.Tasks;

namespace Passport.Business.Service
{
    public class TokenService : BaseService
    {
        private readonly ITokenRepository _tokenRepository;

        public TokenService(ITokenRepository tokenRepository)
        {
            _tokenRepository = tokenRepository;
        }

        public Task Generate(HttpContext httpContext)
        {
            try
            {
                var input = ReadBody<TokenRequest>(httpContext, "POST");
                var result = _tokenRepository.Generate(input);
                WriteResponse(httpContext, result);
            }
            catch (Exception ex)
            {
                WriteErrorResponse(httpContext, ex);
            }

            return Task.FromResult(0);
        }

        public Task Validate(HttpContext httpContext)
        {
            try
            {
                var input = ReadBody<ValidateToken>(httpContext, "POST");
                var result = _tokenRepository.Validate(input);
                WriteResponse(httpContext, result);
            }
            catch (Exception ex)
            {
                WriteErrorResponse(httpContext, ex);
            }

            return Task.FromResult(0);
        }
    }
}