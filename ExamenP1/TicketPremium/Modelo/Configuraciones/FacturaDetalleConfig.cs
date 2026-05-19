using System.Data.Entity.ModelConfiguration;
using TicketPremium.Modelo.Entidades;

namespace TicketPremium.Modelo.Configuraciones
{
    public class FacturaDetalleConfig : EntityTypeConfiguration<FacturaDetalle>
    {
        public FacturaDetalleConfig()
        {
            ToTable("FACTURA_DETALLE");
            HasKey(d => d.Id);

            Property(d => d.CodigoLocalidad)
                .HasMaxLength(50)
                .IsRequired();

            Property(d => d.PrecioUnitario)
                .HasPrecision(18, 2);

            Property(d => d.TotalLocalidad)
                .HasPrecision(18, 2);

            HasRequired(d => d.Factura)
                .WithMany(f => f.Detalles)
                .HasForeignKey(d => d.FacturaId)
                .WillCascadeOnDelete(true);
        }
    }
}
