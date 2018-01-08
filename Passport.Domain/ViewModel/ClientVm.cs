using Passport.Domain.Type;
using System.Collections.Generic;

namespace Passport.Domain.ViewModel
{
    public class ClientVm
    {
        public string ClientId { get; set; }
        public string Name { get; set; }
        public string Secret { get; set; }
        public GrantType GrantType { get; set; }
        public List<string> AllowedScopes { get; set; }

        public int GrantTypeId
        {
            set
            {
                this.GrantType = (GrantType)value;
            }
            get
            {
                return (int)this.GrantType;
            }
        }
    }
}