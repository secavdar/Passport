using Microsoft.AspNetCore.Http;
using Passport.Business.Contract;
using Passport.Business.Service.Base;
using Passport.Domain.ViewModel;
using System;
using System.Threading.Tasks;

namespace Passport.Business.Service
{
    public class ConnectService : BaseService
    {
        private readonly ITokenRepository _tokenRepository;

        public ConnectService(ITokenRepository tokenRepository)
        {
            _tokenRepository = tokenRepository;
        }

        public Task GetResponse(HttpContext httpContext)
        {
            try
            {
                var input = ReadBody<TokenInput>(httpContext, "POST");
                var result = _tokenRepository.Generate(input);
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