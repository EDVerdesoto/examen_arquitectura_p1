using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FederacionFutbol.Servicio.Modelos
{
    public class PartidoFutbol
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Codigo { get; set; }

        [Required]
        [StringLength(100)]
        public string EquipoLocal { get; set; }

        [Required]
        [StringLength(100)]
        public string EquipoVisita { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        [StringLength(200)]
        public string Lugar { get; set; }

        // Relacion 1:N con LocalidadPartido
        public virtual ICollection<LocalidadPartido> Localidades { get; set; } = new List<LocalidadPartido>();
    }
}
