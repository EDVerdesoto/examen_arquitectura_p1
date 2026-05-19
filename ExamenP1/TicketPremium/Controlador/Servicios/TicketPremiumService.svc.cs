using System;
using System.Collections.Generic;
using System.Linq;
using FederacionFutbol.Servicio.DTOs;
using TicketPremium.Controlador.DTOs;
using TicketPremium.Modelo.Contexto;
using TicketPremium.Modelo.Entidades;

namespace TicketPremium.Controlador.Servicios
{
    public class TicketPremiumService : ITicketPremiumService
    {
        private readonly PartidoServiceClient _partidoClient;

        public TicketPremiumService()
        {
            _partidoClient = new PartidoServiceClient();
        }

        public LoginResponse Login(string nombreUsuario, string clave)
        {
            using (var db = new TicketPremiumDbContext())
            {
                var usuario = db.Usuarios.FirstOrDefault(u =>
                    u.NombreDeUsuario == nombreUsuario && u.Clave == clave);

                if (usuario == null)
                {
                    return new LoginResponse
                    {
                        Exito = false,
                        Mensaje = "Usuario o clave incorrectos."
                    };
                }

                return new LoginResponse
                {
                    Exito = true,
                    Mensaje = "Inicio de sesion exitoso.",
                    UsuarioId = usuario.Id
                };
            }
        }

        public List<PartidoDTO> ObtenerPartidosDisponibles()
        {
            return _partidoClient.ObtenerPartidosDisponibles();
        }

        public List<LocalidadDTO> ObtenerLocalidadesPorPartido(int codigoPartido)
        {
            return _partidoClient.ObtenerLocalidadesPorPartido(codigoPartido);
        }

        public CompraResponse ComprarBoletos(CompraRequest request)
        {
            var respuesta = _partidoClient.ComprarBoleto(
                request.CodigoPartido, request.CodigoLocalidad, request.Cantidad);

            if (!respuesta.Exito)
            {
                return new CompraResponse
                {
                    Exito = false,
                    Mensaje = respuesta.Mensaje
                };
            }

            var subtotal = request.PrecioUnitario * request.Cantidad;
            var iva = subtotal * 0.15m;
            var total = subtotal + iva;

            using (var db = new TicketPremiumDbContext())
            {
                var factura = new FacturaCabecera
                {
                    PartidoCodigo = request.CodigoPartido,
                    PartidoDescripcion = request.PartidoDescripcion,
                    FechaPartido = request.FechaPartido,
                    FechaCompra = DateTime.Now,
                    Subtotal = subtotal,
                    Iva = iva,
                    Total = total,
                    UsuarioId = request.UsuarioId
                };

                factura.Detalles.Add(new FacturaDetalle
                {
                    CodigoLocalidad = request.CodigoLocalidad,
                    Cantidad = request.Cantidad,
                    PrecioUnitario = request.PrecioUnitario,
                    TotalLocalidad = subtotal
                });

                db.Facturas.Add(factura);
                db.SaveChanges();

                return new CompraResponse
                {
                    Exito = true,
                    Mensaje = "Compra realizada exitosamente.",
                    FacturaId = factura.Id,
                    Subtotal = subtotal,
                    Iva = iva,
                    Total = total
                };
            }
        }

        public ResumenVentaDTO ObtenerResumenVentas(int codigoPartido)
        {
            var partidos = _partidoClient.ObtenerPartidosDisponibles();
            var partido = partidos.FirstOrDefault(p => p.Codigo == codigoPartido);

            using (var db = new TicketPremiumDbContext())
            {
                var facturaCabecera = db.Facturas
                    .FirstOrDefault(f => f.PartidoCodigo == codigoPartido);

                var fecha = DateTime.MinValue;
                var partidoDescripcion = "Partido no encontrado";

                if (partido != null)
                {
                    partidoDescripcion = partido.EquipoLocal + " vs " + partido.EquipoVisita;
                    fecha = partido.Fecha;
                }
                else if (facturaCabecera != null)
                {
                    partidoDescripcion = facturaCabecera.PartidoDescripcion;
                    fecha = facturaCabecera.FechaPartido;
                }

                var detalles = db.FacturaDetalles
                    .Where(d => d.Factura.PartidoCodigo == codigoPartido)
                    .GroupBy(d => d.CodigoLocalidad)
                    .Select(g => new ResumenVentaDetalleDTO
                    {
                        Localidad = g.Key,
                        Vendidos = g.Sum(d => d.Cantidad),
                        TotalRecaudado = g.Sum(d => d.TotalLocalidad)
                    })
                    .ToList();

                return new ResumenVentaDTO
                {
                    Partido = partidoDescripcion,
                    Fecha = fecha,
                    Detalles = detalles
                };
            }
        }
    }
}
