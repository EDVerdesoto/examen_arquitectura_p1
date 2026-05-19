using System;
using System.Runtime.Serialization;

namespace FederacionFutbol.Servicio.DTOs
{
    [DataContract]
    public class PartidoDTO
    {
        [DataMember]
        public int Codigo { get; set; }

        [DataMember]
        public string EquipoLocal { get; set; }

        [DataMember]
        public string EquipoVisita { get; set; }

        [DataMember]
        public DateTime Fecha { get; set; }

        [DataMember]
        public string Lugar { get; set; }
    }
}
