using Microsoft.AspNetCore.Mvc;
using Passport.Business.Contract;
using Passport.Domain.ViewModel;

namespace Passport.Presentation.Controllers
{
    [Route("Scopes")]
    public class ScopeController : Controller
    {
        private readonly IScopeRepository _scopeRepository;

        public ScopeController(IScopeRepository scopeRepository)
        {
            _scopeRepository = scopeRepository;
        }

        [HttpPost]
        public IActionResult Create([FromBody] ScopeVm scopeVm)
        {
            _scopeRepository.Create(scopeVm);
            return Ok();
        }
    }
}