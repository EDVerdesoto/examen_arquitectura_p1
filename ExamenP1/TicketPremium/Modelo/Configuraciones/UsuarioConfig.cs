using System.Data.Entity.ModelConfiguration;
using TicketPremium.Modelo.Entidades;

namespace TicketPremium.Modelo.Configuraciones
{
    public class UsuarioConfig : EntityTypeConfiguration<Usuario>
    {
        public UsuarioConfig()
        {
            ToTable("USUARIO");
            HasKey(u => u.Id);

            Property(u => u.NombreDeUsuario)
                .HasMaxLength(50)
                .IsRequired();

            Property(u => u.Clave)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
