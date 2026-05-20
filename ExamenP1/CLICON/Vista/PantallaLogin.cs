using CLICON.Controlador;
using CLICON.Modelo;

namespace CLICON.Vista;

public static class PantallaLogin
{
    public static async Task Ejecutar(TicketService servicio, Sesion sesion)
    {
        while (!sesion.IsAuthenticated)
        {
            ConsolaUI.Encabezado();
            ConsolaUI.Titulo("I N I C I A R   S E S I Ó N");

            var usuario = ConsolaUI.LeerTexto("Usuario");
            var clave = ConsolaUI.LeerClave("Clave");

            Console.WriteLine();
            ConsolaUI.Info("Ingresando...");
            Console.WriteLine();

            try
            {
                var respuesta = await servicio.LoginAsync(usuario, clave);

                if (respuesta.Exito)
                {
                    sesion.Login(respuesta.UsuarioId, usuario);
                    ConsolaUI.MostrarExito("Inicio de sesión exitoso.");
                    await Task.Delay(800);
                }
                else
                {
                    ConsolaUI.MostrarError(respuesta.Mensaje ?? "Credenciales inválidas.");
                    ConsolaUI.EsperarTecla();
                }
            }
            catch
            {
                ConsolaUI.MostrarError("Error de conexión con el servidor. Verifique que los servicios estén disponibles.");
                ConsolaUI.EsperarTecla();
            }
        }
    }
}
