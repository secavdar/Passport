using Passport.Domain.Model;
using Passport.Domain.ViewModel;
using System.Collections.Generic;
using System.Linq;

namespace Passport.Business.Extension
{
    public static class ClientExtension
    {
        public static Client IsMatch(this IEnumerable<Client> source, TokenInput tokenInput)
        {
            return source.FirstOrDefault(x => x.Id == tokenInput.ClientId && x.GrantType == tokenInput.GrantType);
        }
    }
}