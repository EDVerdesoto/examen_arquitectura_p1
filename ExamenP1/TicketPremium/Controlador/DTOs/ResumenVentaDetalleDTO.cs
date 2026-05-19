using System.Runtime.Serialization;

namespace TicketPremium.Controlador.DTOs
{
    [DataContract]
    public class ResumenVentaDetalleDTO
    {
        [DataMember]
        public string Localidad { get; set; }

        [DataMember]
        public int Vendidos { get; set; }

        [DataMember]
        public decimal TotalRecaudado { get; set; }
    }
}
