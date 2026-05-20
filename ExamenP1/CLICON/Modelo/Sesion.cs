namespace CLICON.Modelo;

public class Sesion
{
    public int UsuarioId { get; private set; }
    public string NombreUsuario { get; private set; } = string.Empty;
    public bool IsAuthenticated => UsuarioId > 0;

    public void Login(int usuarioId, string nombreUsuario)
    {
        UsuarioId = usuarioId;
        NombreUsuario = nombreUsuario;
    }

    public void Logout()
    {
        UsuarioId = 0;
        NombreUsuario = string.Empty;
    }
}
