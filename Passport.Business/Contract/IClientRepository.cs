using Passport.Domain.ViewModel;
using System.Collections.Generic;

namespace Passport.Business.Contract
{
    public interface IClientRepository
    {
        List<ClientVm> List();
        ClientVm Get(int id);
        ClientVm GetMatched(TokenRequest tokenRequest);
        void Create(ClientVm clientVm);
        void Update(int id, ClientVm clientVm);
    }
}