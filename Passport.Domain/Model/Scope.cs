using System.Collections.Generic;

namespace Passport.Domain.Model
{
    public class Scope
    {
        public Scope()
        {
            this.ClientScopes = new List<ClientScope>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<ClientScope> ClientScopes { get; set; }
    }
}