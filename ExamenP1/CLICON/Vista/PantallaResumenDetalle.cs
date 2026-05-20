using TicketPremiumServiceRef;

namespace CLICON.Vista;

public static class PantallaResumenDetalle
{
    public static Task Ejecutar(PartidoDTO partido, ResumenVentaDTO resumen)
    {
        ConsolaUI.Encabezado();
        ConsolaUI.Titulo("D E T A L L E   D E   V E N T A S");
        Console.WriteLine($"    {partido.EquipoLocal} vs {partido.EquipoVisita}  |  {partido.Fecha:dd/MM/yyyy HH:mm}");
        Console.WriteLine();

        if (resumen.Detalles.Length == 0)
        {
            ConsolaUI.MostrarError("Aún no se han registrado ventas para este partido.");
        }
        else
        {
            ConsolaUI.TablaEncabezado("Localidad", "Boletos vendidos", "Total recaudado");

            foreach (var detalle in resumen.Detalles)
            {
                ConsolaUI.TablaFila(
                    detalle.Localidad,
                    detalle.Vendidos.ToString(),
                    $"${detalle.TotalRecaudado:N2}"
                );
            }

            ConsolaUI.TablaFilaDestacada(
                "TOTALES",
                resumen.Detalles.Sum(d => d.Vendidos).ToString(),
                $"${resumen.Detalles.Sum(d => d.TotalRecaudado):N2}"
            );
        }

        ConsolaUI.EsperarTecla();
        return Task.CompletedTask;
    }
}
