using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using FederacionFutbol.Servicio.DataLayer;
using FederacionFutbol.Servicio.DTOs;
using FederacionFutbol.Servicio.Modelos;

namespace FederacionFutbol.Servicio
{
    public class PartidoService : IPartidoService
    {
        private readonly FederacionDbContext _context;

        public PartidoService()
        {
            _context = new FederacionDbContext();
        }

        public List<PartidoDTO> ObtenerPartidosDisponibles()
        {
            var hoy = DateTime.Now;
            var partidos = _context.Partidos
                .Where(p => p.Fecha >= hoy)
                .OrderBy(p => p.Fecha)
                .Select(p => new PartidoDTO
                {
                    Codigo = p.Codigo,
                    EquipoLocal = p.EquipoLocal,
                    EquipoVisita = p.EquipoVisita,
                    Fecha = p.Fecha,
                    Lugar = p.Lugar
                })
                .ToList();

            return partidos;
        }

        public List<LocalidadDTO> ObtenerLocalidadesPorPartido(int codigoPartido)
        {
            var localidades = _context.Localidades
                .Where(l => l.PartidoCodigo == codigoPartido && l.Disponibilidad > 0)
                .Select(l => new LocalidadDTO
                {
                    CodigoLocalidad = l.CodigoLocalidad,
                    Disponibilidad = l.Disponibilidad,
                    Precio = l.Precio
                })
                .ToList();

            return localidades;
        }

        public RespuestaCompraDTO ComprarBoleto(int codigoPartido, string codigoLocalidad, int cantidad)
        {
            var localidad = _context.Localidades
                .FirstOrDefault(l => l.PartidoCodigo == codigoPartido && l.CodigoLocalidad == codigoLocalidad);

            if (localidad == null)
            {
                return new RespuestaCompraDTO
                {
                    Exito = false,
                    Mensaje = "La localidad especificada no existe para este partido.",
                    DisponibilidadRestante = 0
                };
            }

            if (localidad.Disponibilidad < cantidad)
            {
                return new RespuestaCompraDTO
                {
                    Exito = false,
                    Mensaje = $"Solo quedan {localidad.Disponibilidad} boletos disponibles para la localidad {codigoLocalidad}.",
                    DisponibilidadRestante = localidad.Disponibilidad
                };
            }

            localidad.Disponibilidad -= cantidad;
            _context.SaveChanges();

            return new RespuestaCompraDTO
            {
                Exito = true,
                Mensaje = "Compra realizada exitosamente.",
                DisponibilidadRestante = localidad.Disponibilidad
            };
        }
    }
}
