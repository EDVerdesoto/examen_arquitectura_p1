using System;
using System.Collections.Generic;
using System.ServiceModel;
using FederacionFutbol.Servicio;
using FederacionFutbol.Servicio.DTOs;

namespace TicketPremium.Controlador.Servicios
{
    public class PartidoServiceClient
    {
        private readonly IPartidoService _canal;

        public PartidoServiceClient()
        {
            var binding = new BasicHttpBinding();
            var endpoint = new EndpointAddress("http://localhost:56447/Controlador/Servicios/PartidoService.svc");
            var factory = new ChannelFactory<IPartidoService>(binding, endpoint);
            _canal = factory.CreateChannel();
        }

        public List<PartidoDTO> ObtenerPartidosDisponibles()
        {
            return _canal.ObtenerPartidosDisponibles();
        }

        public List<LocalidadDTO> ObtenerLocalidadesPorPartido(int codigoPartido)
        {
            return _canal.ObtenerLocalidadesPorPartido(codigoPartido);
        }

        public RespuestaCompraDTO ComprarBoleto(int codigoPartido, string codigoLocalidad, int cantidad)
        {
            return _canal.ComprarBoleto(codigoPartido, codigoLocalidad, cantidad);
        }
    }
}
