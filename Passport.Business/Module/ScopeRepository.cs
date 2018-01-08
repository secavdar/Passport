using Passport.Business.Contract;
using Passport.Domain.Model;
using Passport.Domain.ViewModel;
using System.Collections.Generic;
using System.Linq;

namespace Passport.Business.Module
{
    public class ScopeRepository : IScopeRepository
    {
        private readonly PassportContext _passportContext;

        public ScopeRepository(PassportContext passportContext)
        {
            _passportContext = passportContext;
        }

        public List<ScopeVm> List()
        {
            return _passportContext.Set<Scope>()
                                   .Select(x => new ScopeVm
                                   {
                                       Name = x.Name,
                                       Description = x.Description
                                   })
                                   .ToList();
        }
        public ScopeVm Get(int id)
        {
            return _passportContext.Set<Scope>()
                                   .Where(x => x.Id == id)
                                   .Select(x => new ScopeVm
                                   {
                                       Name = x.Name,
                                       Description = x.Description
                                   })
                                   .FirstOrDefault();
        }
        public void Create(ScopeVm scopeVm)
        {
            var entity = new Scope
            {
                Name = scopeVm.Name,
                Description = scopeVm.Description
            };

            _passportContext.Set<Scope>().Add(entity);
            _passportContext.SaveChanges();
        }
        public void Update(int id, ScopeVm scopeVm)
        {
            var entity = _passportContext.Set<Scope>().Find(id);

            entity.Description = scopeVm.Description;

            _passportContext.SaveChanges();
        }
    }
}