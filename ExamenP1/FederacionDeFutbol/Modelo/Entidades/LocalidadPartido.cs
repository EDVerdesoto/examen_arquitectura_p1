using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FederacionFutbol.Servicio.Modelos
{
    public class LocalidadPartido
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string CodigoLocalidad { get; set; }

        [Required]
        public int Disponibilidad { get; set; }

        [Required]
        public decimal Precio { get; set; }

        // Clave foranea
        public int PartidoCodigo { get; set; }

        [ForeignKey("PartidoCodigo")]
        public virtual PartidoFutbol Partido { get; set; }
    }
}
