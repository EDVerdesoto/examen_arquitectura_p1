using System;
using System.Collections.Generic;
using System.ServiceModel;
using FederacionFutbol.Servicio.DTOs;

namespace FederacionFutbol.Servicio
{
    [ServiceContract]
    public interface IPartidoService
    {
        [OperationContract]
        List<PartidoDTO> ObtenerPartidosDisponibles();

        [OperationContract]
        List<LocalidadDTO> ObtenerLocalidadesPorPartido(int codigoPartido);

        [OperationContract]
        RespuestaCompraDTO ComprarBoleto(int codigoPartido, string codigoLocalidad, int cantidad);
    }
}
