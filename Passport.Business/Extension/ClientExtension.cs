using Passport.Domain.Model;
using Passport.Domain.Type;
using Passport.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Passport.Business.Extension
{
    public static class ClientExtension
    {
        public static Client IsMatch(this IEnumerable<Client> source, TokenRequest tokenRequest)
        {
            var grantType = Enum.Parse<GrantType>(tokenRequest.GrantType);
            return source.FirstOrDefault(x => x.Id == tokenRequest.ClientId && x.GrantType == grantType && x.Secret == tokenRequest.ClientSecret);
        }
    }
}