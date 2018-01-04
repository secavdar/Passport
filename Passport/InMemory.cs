using Passport.Domain.Model;
using Passport.Domain.Type;
using System.Collections.Generic;

namespace Passport
{
    public class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new List<Client>
            {
                new Client
                {
                    Id = "TRV",
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
}