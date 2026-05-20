using CLICON.Controlador;
using CLICON.Modelo;
using TicketPremiumServiceRef;

namespace CLICON.Vista;

public static class PantallaPartidos
{
    public static async Task<int> Ejecutar(TicketService servicio, Sesion sesion)
    {
        List<PartidoDTO> partidos;
        try
        {
            partidos = (await servicio.ObtenerPartidosDisponiblesAsync()).ToList();
        }
        catch
        {
            ConsolaUI.MostrarError("No se pudieron cargar los partidos. Verifique la conexión con el servidor.");
            ConsolaUI.EsperarTecla();
            return 0;
        }

        if (partidos.Count == 0)
        {
            ConsolaUI.MostrarError("No hay partidos disponibles en este momento.");
            ConsolaUI.EsperarTecla();
            return 0;
        }

        while (true)
        {
            ConsolaUI.Encabezado();
            ConsolaUI.Titulo("P A R T I D O S   D I S P O N I B L E S");

            for (int i = 0; i < partidos.Count; i++)
            {
                var p = partidos[i];
                Console.WriteLine($"  {i + 1,3}. {p.EquipoLocal,-20} VS  {p.EquipoVisita}");
                Console.WriteLine($"       {p.Fecha:dd/MM/yyyy HH:mm}  |  {p.Lugar}");
                Console.WriteLine();
            }

            ConsolaUI.Opcion("0", "Volver al menú");

            var opcion = ConsolaUI.LeerEntero("Seleccione un partido", 0, partidos.Count);

            if (opcion == 0) return 0;

            var codigo = partidos[opcion - 1].Codigo;
            var resultado = await PantallaLocalidades.Ejecutar(servicio, sesion, codigo);

            if (resultado == -1) return -1;
        }
    }
}
