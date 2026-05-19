using System.Data.Entity.ModelConfiguration;
using TicketPremium.Modelo.Entidades;

namespace TicketPremium.Modelo.Configuraciones
{
    public class FacturaCabeceraConfig : EntityTypeConfiguration<FacturaCabecera>
    {
        public FacturaCabeceraConfig()
        {
            ToTable("FACTURA_CABECERA");
            HasKey(f => f.Id);

            Property(f => f.PartidoDescripcion)
                .HasMaxLength(200)
                .IsRequired();

            Property(f => f.Subtotal)
                .HasPrecision(18, 2);

            Property(f => f.Iva)
                .HasPrecision(18, 2);

            Property(f => f.Total)
                .HasPrecision(18, 2);

            HasRequired(f => f.Usuario)
                .WithMany(u => u.Facturas)
                .HasForeignKey(f => f.UsuarioId)
                .WillCascadeOnDelete(true);
        }
    }
}
