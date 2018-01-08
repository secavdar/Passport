using Passport.Domain.Type;
using Passport.Domain.ViewModel;
using System.Collections.Generic;

namespace Passport
{
    public class Clients
    {
        public static IEnumerable<ClientVm> Get()
        {
            return new List<ClientVm>
            {
                new ClientVm
                {
                    ClientId = "TRV",
                    Name = "TRV",
                    GrantType = GrantType.ClientCredentials,
                    Secret = "secret",
                    AllowedScopes = new List<string>
                    {
                        "Middleware.TRV",
                        "Middleware.HRP"
                    }
                }
            };
        }
    }
    public class Scopes
    {
        public static IEnumerable<ScopeVm> Get()
        {
            return new List<ScopeVm>
            {
                new ScopeVm
                {
                    Name = "Middleware.TRV",
                    Description = "Middleware.TRV"
                },
                new ScopeVm
                {
                    Name = "Middleware.HRP",
                    Description = "Middleware.HRP"
                }
            };
        }
    }
}