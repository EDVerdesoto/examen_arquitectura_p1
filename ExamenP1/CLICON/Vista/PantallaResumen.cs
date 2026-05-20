using CLICON.Controlador;
using TicketPremiumServiceRef;

namespace CLICON.Vista;

public static class PantallaResumen
{
    public static async Task Ejecutar(TicketService servicio)
    {
        List<(PartidoDTO Partido, ResumenVentaDTO Resumen)> resumenes;

        try
        {
            var partidos = await servicio.ObtenerPartidosDisponiblesAsync();
            resumenes = new();

            foreach (var partido in partidos)
            {
                try
                {
                    var resumen = await servicio.ObtenerResumenVentasAsync(partido.Codigo);
                    resumenes.Add((partido, resumen));
                }
                catch
                {
                    // omitir partidos que fallan
                }
            }

            resumenes = resumenes.OrderByDescending(r => r.Partido.Fecha).ToList();
        }
        catch
        {
            ConsolaUI.MostrarError("No se pudo cargar la información de ventas. Verifique la conexión.");
            ConsolaUI.EsperarTecla();
            return;
        }

        while (true)
        {
            ConsolaUI.Encabezado();
            ConsolaUI.Titulo("R E S U M E N   D E   V E N T A S");

            if (resumenes.Count == 0)
            {
                ConsolaUI.MostrarError("No hay información de ventas disponible.");
                ConsolaUI.EsperarTecla();
                return;
            }

            for (int i = 0; i < resumenes.Count; i++)
            {
                var (partido, resumen) = resumenes[i];
                var totalVendidos = resumen.Detalles.Sum(d => d.Vendidos);
                var totalRecaudado = resumen.Detalles.Sum(d => d.TotalRecaudado);

                Console.WriteLine($"  {i + 1,3}. {partido.EquipoLocal} vs {partido.EquipoVisita}");
                Console.WriteLine($"       {totalVendidos,5} vendidos | ${totalRecaudado:N0} recaudado");
                Console.WriteLine();
            }

            Console.WriteLine();
            ConsolaUI.Opcion("0", "Volver al menú");

            var opcion = ConsolaUI.LeerEntero("Seleccione un partido para ver detalle", 0, resumenes.Count);

            if (opcion == 0) return;

            var (partidoSel, resumenSel) = resumenes[opcion - 1];
            await PantallaResumenDetalle.Ejecutar(partidoSel, resumenSel);
        }
    }
}
