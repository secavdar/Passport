using Microsoft.EntityFrameworkCore;

namespace Passport.Domain.Model
{
    public class PassportContext : DbContext
    {
        public PassportContext() { }
        public PassportContext(DbContextOptions<PassportContext> options) : base(options) { }

        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<ClientScope> ClientScopes { get; set; }
        public virtual DbSet<ClientSecret> ClientSecrets { get; set; }
        public virtual DbSet<Scope> Scopes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClientSecret>(entity =>
            {
                entity.HasOne(x => x.Client)
                    .WithMany(x => x.ClientSecrets)
                    .HasForeignKey(x => x.ClientId);
            });

            modelBuilder.Entity<ClientScope>(entity =>
            {
                entity.HasOne(x => x.Client)
                    .WithMany(x => x.ClientScopes)
                    .HasForeignKey(x => x.ClientId);

                entity.HasOne(x => x.Scope)
                    .WithMany(x => x.ClientScopes)
                    .HasForeignKey(x => x.ScopeId);

                entity.HasKey(x => new { x.ClientId, x.ScopeId });
            });
        }
    }
}