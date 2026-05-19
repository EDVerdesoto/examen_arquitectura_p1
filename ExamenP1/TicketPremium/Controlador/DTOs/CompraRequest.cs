using System;
using System.Runtime.Serialization;

namespace TicketPremium.Controlador.DTOs
{
    [DataContract]
    public class CompraRequest
    {
        [DataMember]
        public int UsuarioId { get; set; }

        [DataMember]
        public int CodigoPartido { get; set; }

        [DataMember]
        public string PartidoDescripcion { get; set; }

        [DataMember]
        public DateTime FechaPartido { get; set; }

        [DataMember]
        public string CodigoLocalidad { get; set; }

        [DataMember]
        public decimal PrecioUnitario { get; set; }

        [DataMember]
        public int Cantidad { get; set; }
    }
}
