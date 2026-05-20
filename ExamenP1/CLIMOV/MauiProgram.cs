using CLIMOV.Controlador;
using CLIMOV.Vistas;
using Microsoft.Extensions.Logging;
using TicketPremiumServiceRef;

namespace CLIMOV
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<Sesion>();
            builder.Services.AddTransient<TicketPremiumServiceClient>();
            builder.Services.AddTransient<TicketService>();

            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<PartidosPage>();
            builder.Services.AddTransient<LocalidadesPage>();
            builder.Services.AddTransient<ComprarPage>();
            builder.Services.AddTransient<ResumenPage>();
            builder.Services.AddTransient<ResumenVentasPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
