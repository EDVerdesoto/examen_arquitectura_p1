using System.Runtime.Serialization;

namespace TicketPremium.Controlador.DTOs
{
    [DataContract]
    public class CompraResponse
    {
        [DataMember]
        public bool Exito { get; set; }

        [DataMember]
        public string Mensaje { get; set; }

        [DataMember]
        public int FacturaId { get; set; }

        [DataMember]
        public decimal Subtotal { get; set; }

        [DataMember]
        public decimal Iva { get; set; }

        [DataMember]
        public decimal Total { get; set; }
    }
}
