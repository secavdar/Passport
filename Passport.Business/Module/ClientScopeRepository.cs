using Passport.Business.Contract;
using Passport.Business.Extension;
using Passport.Domain.Model;
using Passport.Domain.ViewModel;
using System.Linq;

namespace Passport.Business.Module
{
    public class ClientScopeRepository : IClientScopeRepository
    {
        private readonly PassportContext _passportContext;

        public ClientScopeRepository(PassportContext passportContext)
        {
            _passportContext = passportContext;
        }

        public void Create(ClientVm clientVm)
        {
            var client = _passportContext.Set<Client>()
                                         .FirstOrDefault(x => x.Code == clientVm.ClientId);

            _passportContext.Set<ClientScope>()
                            .DeleteMany(x => x.ClientId == client.Id);

            _passportContext.SaveChanges();

            foreach (var scopeName in clientVm.AllowedScopes)
            {
                var scope = _passportContext.Set<Scope>()
                                            .FirstOrDefault(x => x.Name == scopeName);

                var clientScope = new ClientScope
                {
                    ClientId = client.Id,
                    ScopeId = scope.Id
                };

                _passportContext.Set<ClientScope>().Add(clientScope);
            }

            _passportContext.SaveChanges();
        }
    }
}