using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TicketPremium.Controlador.DTOs
{
    [DataContract]
    public class ResumenVentaDTO
    {
        [DataMember]
        public string Partido { get; set; }

        [DataMember]
        public DateTime Fecha { get; set; }

        [DataMember]
        public List<ResumenVentaDetalleDTO> Detalles { get; set; }
    }
}
