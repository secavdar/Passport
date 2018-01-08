using Passport.Business.Contract;
using Passport.Business.Internal;
using Passport.Domain.Model;
using Passport.Domain.Type;
using Passport.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Passport.Business.Module
{
    public class ClientRepository : IClientRepository
    {
        private readonly PassportContext _passportContext;
        private readonly IClientScopeRepository _clientScopeRepository;
        private readonly IClientSecretRepository _clientSecretRepository;
        private readonly ICryptoRepository _cryptoRepository;

        public ClientRepository(PassportContext passportContext, IClientScopeRepository clientScopeRepository, IClientSecretRepository clientSecretRepository, ICryptoRepository cryptoRepository)
        {
            _passportContext = passportContext;
            _clientScopeRepository = clientScopeRepository;
            _clientSecretRepository = clientSecretRepository;
            _cryptoRepository = cryptoRepository;
        }

        public List<ClientVm> List()
        {
            return _passportContext.Set<Client>()
                                   .Select(x => new ClientVm
                                   {
                                       ClientId = x.Code,
                                       Name = x.Name,
                                       GrantTypeId = x.GrantTypeId,
                                       AllowedScopes = x.ClientScopes
                                                        .Select(y => y.Scope.Name)
                                                        .ToList()
                                   })
                                   .ToList();
        }
        public ClientVm Get(int id)
        {
            return _passportContext.Set<Client>()
                                   .Where(x => x.Id == id)
                                   .Select(x => new ClientVm
                                   {
                                       ClientId = x.Code,
                                       Name = x.Name,
                                       GrantTypeId = x.GrantTypeId,
                                       AllowedScopes = x.ClientScopes
                                                        .Select(y => y.Scope.Name)
                                                        .ToList()
                                   })
                                   .FirstOrDefault();
        }
        public ClientVm GetMatched(TokenRequest tokenRequest)
        {
            var grantTypeId = (int)Enum.Parse<GrantType>(tokenRequest.GrantType);
            var hashedSecret = _cryptoRepository.Hash(tokenRequest.ClientSecret);

            return _passportContext.Set<Client>()
                                   .Where(x => x.Code == tokenRequest.ClientId && x.GrantTypeId == grantTypeId && x.ClientSecrets.Any(y => y.Secret == hashedSecret))
                                   .Select(x => new ClientVm
                                   {
                                       ClientId = x.Code,
                                       Name = x.Name,
                                       GrantTypeId = x.GrantTypeId,
                                       AllowedScopes = x.ClientScopes
                                                        .Select(y => y.Scope.Name)
                                                        .ToList()
                                   })
                                   .FirstOrDefault();
        }
        public void Create(ClientVm clientVm)
        {
            var client = new Client
            {
                Code = clientVm.ClientId,
                Name = clientVm.Name,
                GrantTypeId = clientVm.GrantTypeId
            };

            _passportContext.Set<Client>().Add(client);
            _passportContext.SaveChanges();

            _clientSecretRepository.Create(clientVm);
            _clientScopeRepository.Create(clientVm);
        }
        public void Update(int id, ClientVm clientVm)
        {
            var client = _passportContext.Set<Client>().Find(id);

            client.Name = clientVm.Name;
            client.GrantTypeId = clientVm.GrantTypeId;

            _passportContext.SaveChanges();

            _clientSecretRepository.Create(clientVm);
            _clientScopeRepository.Create(clientVm);
        }
    }
}