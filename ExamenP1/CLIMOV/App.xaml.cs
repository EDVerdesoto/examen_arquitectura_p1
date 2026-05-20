using CLIMOV.Controlador;

namespace CLIMOV
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
            return new Window(new AppShell(_sesion));
        }
    }
}
