using Microsoft.AspNetCore.Mvc;
using Passport.Business.Contract;
using Passport.Domain.ViewModel;

namespace Passport.Presentation.Controllers
{
    [Route("Token")]
    public class TokenController : Controller
    {
        private readonly ITokenRepository _tokenRepository;

        public TokenController(ITokenRepository tokenRepository)
        {
            _tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("Generate")]
        public IActionResult Generate([FromBody] TokenRequest tokenRequest)
        {
            var result = _tokenRepository.Generate(tokenRequest);
            return Ok(result);
        }

        [HttpPost]
        [Route("Validate")]
        public IActionResult Validate([FromBody] ValidateToken validateToken)
        {
            var result = _tokenRepository.Validate(validateToken);
            return Ok(result);
        }
    }
}