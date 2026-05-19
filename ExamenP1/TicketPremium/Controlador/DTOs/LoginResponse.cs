using System.Runtime.Serialization;

namespace TicketPremium.Controlador.DTOs
{
    [DataContract]
    public class LoginResponse
    {
        [DataMember]
        public bool Exito { get; set; }

        [DataMember]
        public string Mensaje { get; set; }

        [DataMember]
        public int UsuarioId { get; set; }
    }
}
