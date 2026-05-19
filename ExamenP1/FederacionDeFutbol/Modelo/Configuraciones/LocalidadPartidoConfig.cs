using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using FederacionFutbol.Servicio.Modelos;

namespace FederacionDeFutbol.Modelo.Configuraciones
{
    public class LocalidadPartidoConfig : EntityTypeConfiguration<LocalidadPartido>
    {
        public LocalidadPartidoConfig()
        {
            ToTable("LOCALIDAD_PARTIDO");
            HasKey(l => l.Id);

            Property(l => l.CodigoLocalidad)
                .HasMaxLength(50)
                .IsRequired();

            Property(l => l.Disponibilidad)
                .IsRequired();

            Property(l => l.Precio)
                .HasPrecision(18, 2)
                .IsRequired();

            HasRequired(l => l.Partido)
                .WithMany(p => p.Localidades)
                .HasForeignKey(l => l.PartidoCodigo)
                .WillCascadeOnDelete(true);
        }
    }
}