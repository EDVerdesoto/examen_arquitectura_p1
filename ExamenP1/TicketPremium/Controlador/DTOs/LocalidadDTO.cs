using System.Runtime.Serialization;

namespace FederacionFutbol.Servicio.DTOs
{
    [DataContract]
    public class LocalidadDTO
    {
        [DataMember]
        public string CodigoLocalidad { get; set; }

        [DataMember]
        public int Disponibilidad { get; set; }

        [DataMember]
        public decimal Precio { get; set; }
    }
}
