using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TicketPremium.Modelo.Entidades
{
    public class FacturaCabecera
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int PartidoCodigo { get; set; }

        [Required]
        [StringLength(200)]
        public string PartidoDescripcion { get; set; }

        [Required]
        public DateTime FechaPartido { get; set; }

        [Required]
        public DateTime FechaCompra { get; set; } = DateTime.Now;

        [Required]
        public decimal Subtotal { get; set; }

        [Required]
        public decimal Iva { get; set; }

        [Required]
        public decimal Total { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public virtual Usuario Usuario { get; set; }

        public virtual ICollection<FacturaDetalle> Detalles { get; set; } = new List<FacturaDetalle>();
    }
}