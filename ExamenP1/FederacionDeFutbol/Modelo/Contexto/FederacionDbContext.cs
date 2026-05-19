using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Reflection;
using FederacionFutbol.Servicio.Modelos;

namespace FederacionFutbol.Servicio.DataLayer
{
    public class FederacionDbContext : DbContext
    {
        public FederacionDbContext() : base("name=FederacionDbContext")
        {
        }

        public DbSet<PartidoFutbol> Partidos { get; set; }
        public DbSet<LocalidadPartido> Localidades { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
