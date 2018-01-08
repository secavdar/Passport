using Passport.Domain.ViewModel;
using System.Collections.Generic;

namespace Passport.Business.Contract
{
    public interface IScopeRepository
    {
        List<ScopeVm> List();
        ScopeVm Get(int id);
        void Create(ScopeVm scopeVm);
        void Update(int id, ScopeVm scopeVm);
    }
}