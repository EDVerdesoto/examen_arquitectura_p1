using CLIESC.Controlador;
using CLIESC.Vistas;

namespace CLIESC;

public partial class AppShell : Shell
{
    private readonly Sesion _sesion;

    public AppShell(Sesion sesion)
    {
        InitializeComponent();
        _sesion = sesion;

        Routing.RegisterRoute("LocalidadesPage", typeof(LocalidadesPage));
        Routing.RegisterRoute("ComprarPage", typeof(ComprarPage));
        Routing.RegisterRoute("ResumenPage", typeof(ResumenPage));
        Routing.RegisterRoute("ResumenVentasPage", typeof(ResumenVentasPage));

        if (_sesion.IsAuthenticated)
        {
            CurrentItem = PartidosShell;
        }
        else
        {
            CurrentItem = LoginShell;
        }
    }
}
