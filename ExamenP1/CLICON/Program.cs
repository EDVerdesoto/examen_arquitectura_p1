using System.Globalization;
using CLICON.Controlador;
using CLICON.Modelo;
using CLICON.Vista;

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("es");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("es");

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.Title = "TicketPremium";

var sesion = new Sesion();
var servicio = new TicketService();

while (true)
{
    if (!sesion.IsAuthenticated)
    {
        await PantallaLogin.Ejecutar(servicio, sesion);
        continue;
    }

    var opcion = await PantallaMenu.Ejecutar(sesion);

    if (opcion == 0 || !sesion.IsAuthenticated)
        continue; // logout o volver al login

    switch (opcion)
    {
        case 1: // Partidos
            await PantallaPartidos.Ejecutar(servicio, sesion);
            break;
        case 2: // Resumen
            await PantallaResumen.Ejecutar(servicio);
            break;
    }
}
