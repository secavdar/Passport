using Passport.Business.Contract;
using Passport.Domain.ViewModel;
using System;
using System.Collections.Generic;

namespace Passport.Business.Module
{
    public class ClientMemoryRepository : IClientRepository
    {
        public List<ClientVm> List()
        {
            throw new NotImplementedException();
        }
        public ClientVm Get(int id)
        {
            throw new NotImplementedException();
        }
        public ClientVm GetMatched(TokenRequest tokenRequest)
        {
            throw new NotImplementedException();
        }
        public void Create(ClientVm clientVm)
        {
            throw new NotImplementedException();
        }
        public void Update(int id, ClientVm clientVm)
        {
            throw new NotImplementedException();
        }
    }
}