namespace Passport.Domain.Model
{
    public class ClientSecret
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Secret { get; set; }

        public virtual Client Client { get; set; }
    }
}