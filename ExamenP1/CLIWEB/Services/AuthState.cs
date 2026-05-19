namespace CLIWEB.Services;

public class AuthState
{
    public int UsuarioId { get; private set; }
    public string NombreUsuario { get; private set; } = string.Empty;
    public bool IsAuthenticated => UsuarioId > 0;

    public event Action? OnChange;

    public void Login(int usuarioId, string nombreUsuario)
    {
        UsuarioId = usuarioId;
        NombreUsuario = nombreUsuario;
        NotifyStateChanged();
    }

    public void Logout()
    {
        UsuarioId = 0;
        NombreUsuario = string.Empty;
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}
