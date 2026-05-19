using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketPremium.Modelo.Entidades
{
    public class FacturaDetalle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int FacturaId { get; set; }

        [Required]
        [StringLength(50)]
        public string CodigoLocalidad { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        public decimal PrecioUnitario { get; set; }

        [Required]
        public decimal TotalLocalidad { get; set; }

        [ForeignKey("FacturaId")]
        public virtual FacturaCabecera Factura { get; set; }
    }
}