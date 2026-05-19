using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Reflection;
using TicketPremium.Modelo.Entidades;

namespace TicketPremium.Modelo.Contexto
{
    public class TicketPremiumDbContext : DbContext
    {
        public TicketPremiumDbContext() : base("name=TicketPremiumDbContext")
        {
        }

        public DbSet<FacturaCabecera> Facturas { get; set; }
        public DbSet<FacturaDetalle> FacturaDetalles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
