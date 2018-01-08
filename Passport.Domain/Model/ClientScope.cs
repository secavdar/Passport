namespace Passport.Domain.Model
{
    public class ClientScope
    {
        public int ClientId { get; set; }
        public int ScopeId { get; set; }

        public virtual Client Client { get; set; }
        public virtual Scope Scope { get; set; }
    }
}