using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using FederacionFutbol.Servicio.Modelos;

namespace FederacionDeFutbol.Modelo.Configuraciones
{
    public class PartidoFutbolConfig : EntityTypeConfiguration<PartidoFutbol>
    {
        public PartidoFutbolConfig()
        {
            ToTable("PARTIDO_FUTBOL");
            HasKey(p => p.Codigo);

            Property(p => p.EquipoLocal)
                .HasMaxLength(100)
                .IsRequired();

            Property(p => p.EquipoVisita)
                .HasMaxLength(100)
                .IsRequired();

            Property(p => p.Fecha)
                .IsRequired();

            Property(p => p.Lugar)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}