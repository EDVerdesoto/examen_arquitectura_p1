using CLICON.Controlador;
using CLICON.Modelo;
using TicketPremiumServiceRef;

namespace CLICON.Vista;

public static class PantallaLocalidades
{
    public static async Task<int> Ejecutar(TicketService servicio, Sesion sesion, int codigoPartido)
    {
        List<LocalidadDTO> localidades;
        PartidoDTO? partido;

        try
        {
            var partidos = await servicio.ObtenerPartidosDisponiblesAsync();
            partido = partidos.FirstOrDefault(p => p.Codigo == codigoPartido);

            if (partido == null)
            {
                ConsolaUI.MostrarError("Partido no encontrado.");
                ConsolaUI.EsperarTecla();
                return 0;
            }

            localidades = (await servicio.ObtenerLocalidadesPorPartidoAsync(codigoPartido)).ToList();
        }
        catch
        {
            ConsolaUI.MostrarError("No se pudieron cargar las localidades. Verifique la conexión.");
            ConsolaUI.EsperarTecla();
            return 0;
        }

        if (localidades.Count == 0)
        {
            ConsolaUI.MostrarError("No hay localidades disponibles para este partido.");
            ConsolaUI.EsperarTecla();
            return 0;
        }

        while (true)
        {
            ConsolaUI.Encabezado();
            ConsolaUI.Titulo("L O C A L I D A D E S   D I S P O N I B L E S");
            Console.WriteLine($"    {partido!.EquipoLocal} vs {partido.EquipoVisita}  |  {partido.Fecha:dd/MM/yyyy HH:mm}  |  {partido.Lugar}");
            Console.WriteLine();

            for (int i = 0; i < localidades.Count; i++)
            {
                var loc = localidades[i];
                var agotado = loc.Disponibilidad == 0 ? "  [AGOTADO]" : "";
                Console.WriteLine($"  {i + 1,3}. {loc.CodigoLocalidad,-14} {loc.Disponibilidad,4} disponibles        ${loc.Precio:N2}{agotado}");
            }

            Console.WriteLine();
            ConsolaUI.Opcion("0", "Volver a partidos");

            var opcion = ConsolaUI.LeerEntero("Seleccione una localidad", 0, localidades.Count);

            if (opcion == 0) return 0;

            var locSeleccionada = localidades[opcion - 1];

            if (locSeleccionada.Disponibilidad == 0)
            {
                ConsolaUI.MostrarError("Esa localidad está agotada.");
                ConsolaUI.EsperarTecla();
                continue;
            }

            await PantallaComprar.Ejecutar(servicio, sesion, codigoPartido, locSeleccionada, partido);

            try
            {
                localidades = (await servicio.ObtenerLocalidadesPorPartidoAsync(codigoPartido)).ToList();
            }
            catch { }
        }
    }
}
