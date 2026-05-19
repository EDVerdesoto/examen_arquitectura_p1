using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketPremium.Modelo.Entidades
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string NombreDeUsuario { get; set; }

        [Required]
        [StringLength(100)]
        public string Clave { get; set; }

        public virtual ICollection<FacturaCabecera> Facturas { get; set; } = new List<FacturaCabecera>();
    }
}