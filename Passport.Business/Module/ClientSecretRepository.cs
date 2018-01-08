using Passport.Business.Contract;
using Passport.Business.Extension;
using Passport.Business.Internal;
using Passport.Domain.Model;
using Passport.Domain.ViewModel;
using System.Linq;

namespace Passport.Business.Module
{
    public class ClientSecretRepository : IClientSecretRepository
    {
        private readonly ICryptoRepository _cryptoRepository;
        private readonly PassportContext _passportContext;

        public ClientSecretRepository(ICryptoRepository cryptoRepository, PassportContext passportContext)
        {
            _cryptoRepository = cryptoRepository;
            _passportContext = passportContext;
        }

        public void Create(ClientVm clientVm)
        {
            var client = _passportContext.Set<Client>()
                                         .FirstOrDefault(x => x.Code == clientVm.ClientId);

            _passportContext.Set<ClientSecret>()
                            .DeleteMany(x => x.ClientId == client.Id);

            _passportContext.SaveChanges();

            var hashedSecret = _cryptoRepository.Hash(clientVm.Secret);

            var clientSecret = new ClientSecret
            {
                ClientId = client.Id,
                Secret = hashedSecret
            };

            _passportContext.Set<ClientSecret>().Add(clientSecret);
            _passportContext.SaveChanges();
        }
    }
}