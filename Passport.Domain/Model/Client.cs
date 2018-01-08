using System.Collections.Generic;

namespace Passport.Domain.Model
{
    public class Client
    {
        public Client()
        {
            this.ClientScopes = new List<ClientScope>();
            this.ClientSecrets = new List<ClientSecret>();
        }

        public int Id { get; set; }
        public int GrantTypeId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ClientScope> ClientScopes { get; set; }
        public virtual ICollection<ClientSecret> ClientSecrets { get; set; }
    }
}