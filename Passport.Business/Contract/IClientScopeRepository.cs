using Passport.Domain.ViewModel;

namespace Passport.Business.Contract
{
    public interface IClientScopeRepository
    {
        void Create(ClientVm clientVm);
    }
}