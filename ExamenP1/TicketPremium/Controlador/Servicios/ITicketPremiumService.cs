using System.Collections.Generic;
using System.ServiceModel;
using FederacionFutbol.Servicio.DTOs;
using TicketPremium.Controlador.DTOs;

namespace TicketPremium.Controlador.Servicios
{
    [ServiceContract]
    public interface ITicketPremiumService
    {
        [OperationContract]
        LoginResponse Login(string nombreUsuario, string clave);

        [OperationContract]
        List<PartidoDTO> ObtenerPartidosDisponibles();

        [OperationContract]
        List<LocalidadDTO> ObtenerLocalidadesPorPartido(int codigoPartido);

        [OperationContract]
        CompraResponse ComprarBoletos(CompraRequest request);

        [OperationContract]
        ResumenVentaDTO ObtenerResumenVentas(int codigoPartido);
    }
}
