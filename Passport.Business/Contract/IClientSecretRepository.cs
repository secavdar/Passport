using Passport.Domain.ViewModel;

namespace Passport.Business.Contract
{
    public interface IClientSecretRepository
    {
        void Create(ClientVm clientVm);
    }
}