using CLICON.Modelo;

namespace CLICON.Vista;

public static class PantallaMenu
{
    public static async Task<int> Ejecutar(Sesion sesion)
    {
        while (sesion.IsAuthenticated)
        {
            ConsolaUI.Encabezado();
            ConsolaUI.Campo("Usuario:", sesion.NombreUsuario);
            Console.WriteLine();
            ConsolaUI.Titulo("M E N Ú   P R I N C I P A L");

            ConsolaUI.Opcion("1", "Ver partidos");
            ConsolaUI.Opcion("2", "Resumen de ventas");
            ConsolaUI.Opcion("3", "Cerrar sesión");

            var opcion = ConsolaUI.LeerEntero("Opción", 1, 3);

            if (opcion == 3)
            {
                sesion.Logout();
                Console.WriteLine();
                ConsolaUI.Info("Cerrando sesión...");
                await Task.Delay(600);
                Console.ResetColor();
                return 0;
            }

            return opcion;
        }

        return 0;
    }
}
