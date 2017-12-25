using Passport.Domain.Type;
using System.Collections.Generic;

namespace Passport.Domain.Model
{
    public class Client
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Secret { get; set; }
        public GrantType GrantType { get; set; }
        public List<string> AllowedScopes { get; set; }
    }
}