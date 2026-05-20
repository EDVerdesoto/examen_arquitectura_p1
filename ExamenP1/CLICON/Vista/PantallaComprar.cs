using CLICON.Controlador;
using CLICON.Modelo;
using TicketPremiumServiceRef;

namespace CLICON.Vista;

public static class PantallaComprar
{
    public static async Task Ejecutar(TicketService servicio, Sesion sesion, int codigoPartido, LocalidadDTO localidad, PartidoDTO partido)
    {
        while (true)
        {
            ConsolaUI.Encabezado();
            ConsolaUI.Titulo("C O M P R A R   B O L E T O S");
            Console.WriteLine($"    {partido.EquipoLocal} vs {partido.EquipoVisita}  |  {partido.Fecha:dd/MM/yyyy HH:mm}");
            Console.WriteLine();

            ConsolaUI.Campo("Localidad:", localidad.CodigoLocalidad);
            ConsolaUI.Campo("Precio unitario:", $"${localidad.Precio:N2}");
            ConsolaUI.Campo("Disponibles:", localidad.Disponibilidad.ToString());
            Console.WriteLine();

            var cantidad = ConsolaUI.LeerEntero($"Cantidad de boletos (máx. {localidad.Disponibilidad})", 1, localidad.Disponibilidad);
            Console.WriteLine();

            var subtotal = cantidad * localidad.Precio;
            var iva = subtotal * 0.15m;
            var total = subtotal + iva;

            ConsolaUI.Linea();
            ConsolaUI.Campo("Subtotal:", $"${subtotal:N2}");
            ConsolaUI.Campo("IVA (15%):", $"${iva:N2}");
            ConsolaUI.Linea();
            ConsolaUI.Campo("Total:", $"${total:N2}");
            ConsolaUI.Linea();
            Console.WriteLine();

            if (!ConsolaUI.LeerSiNo("¿Confirmar compra?"))
            {
                return;
            }

            Console.WriteLine();
            ConsolaUI.Info("Procesando compra...");
            Console.WriteLine();

            try
            {
                var request = new CompraRequest
                {
                    UsuarioId = sesion.UsuarioId,
                    CodigoPartido = codigoPartido,
                    CodigoLocalidad = localidad.CodigoLocalidad,
                    Cantidad = cantidad,
                    PrecioUnitario = localidad.Precio,
                    PartidoDescripcion = $"Partido #{codigoPartido} - {localidad.CodigoLocalidad}",
                    FechaPartido = DateTime.Now
                };

                var response = await servicio.ComprarBoletosAsync(request);

                if (response.Exito)
                {
                    MostrarExito(response, localidad, cantidad);
                    return;
                }
                else
                {
                    ConsolaUI.MostrarError(response.Mensaje ?? "La compra no pudo ser procesada.");
                    ConsolaUI.EsperarTecla();
                    return;
                }
            }
            catch
            {
                ConsolaUI.MostrarError("Error al procesar la compra. Verifique la conexión.");
                ConsolaUI.EsperarTecla();
                return;
            }
        }
    }

    private static void MostrarExito(CompraResponse response, LocalidadDTO localidad, int cantidad)
    {
        ConsolaUI.Encabezado();
        ConsolaUI.Titulo("C O M P R A   E X I T O S A");
        Console.WriteLine();

        ConsolaUI.MostrarExito("Compra realizada con éxito.");
        Console.WriteLine();

        ConsolaUI.Campo("Factura #:", response.FacturaId.ToString());
        ConsolaUI.Campo("Localidad:", localidad.CodigoLocalidad);
        ConsolaUI.Campo("Cantidad:", cantidad.ToString());
        Console.WriteLine();
        ConsolaUI.Linea();
        ConsolaUI.Campo("Subtotal:", $"${response.Subtotal:N2}");
        ConsolaUI.Campo("IVA (15%):", $"${response.Iva:N2}");
        ConsolaUI.Linea();
        ConsolaUI.Campo("Total:", $"${response.Total:N2}");
        ConsolaUI.Linea();

        ConsolaUI.EsperarTecla("Presione cualquier tecla para volver al menú...");
    }
}
