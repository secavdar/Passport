using Microsoft.AspNetCore.Mvc;
using Passport.Business.Contract;
using Passport.Domain.ViewModel;

namespace Passport.Presentation.Controllers
{
    [Route("Clients")]
    public class ClientController : Controller
    {
        private readonly IClientRepository _clientRepository;

        public ClientController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        [HttpPost]
        public IActionResult Create([FromBody] ClientVm clientVm)
        {
            _clientRepository.Create(clientVm);
            return Ok();
        }
    }
}