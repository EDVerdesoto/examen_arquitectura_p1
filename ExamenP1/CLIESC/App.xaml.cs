using CLIESC.Controlador;

namespace CLIESC
{
    public partial class App : Application
    {
        private readonly Sesion _sesion;

        public App(Sesion sesion)
        {
            InitializeComponent();
            _sesion = sesion;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var window = new Window(new AppShell(_sesion));
            window.MinimumWidth = 1024;
            window.MinimumHeight = 720;
            window.Title = "TicketPremium";
            return window;
        }
    }
}
