using System;
using System.Runtime.Serialization;

namespace FederacionFutbol.Servicio.DTOs
{
    [DataContract]
    public class RespuestaCompraDTO
    {
        [DataMember]
        public bool Exito { get; set; }

        [DataMember]
        public string Mensaje { get; set; }

        [DataMember]
        public int DisponibilidadRestante { get; set; }
    }
}
