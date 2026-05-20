using CLIMOV.Controlador;
using CLIMOV.Modelo;

namespace CLIMOV.Vistas;

public partial class LoginPage : ContentPage
{
    private readonly TicketService _ticketService;
    private readonly Sesion _sesion;
    private readonly LoginModel _loginModel = new();
    private bool _isLoading;

    public LoginPage(TicketService ticketService, Sesion sesion)
    {
        InitializeComponent();
        _ticketService = ticketService;
        _sesion = sesion;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (_sesion.IsAuthenticated)
        {
            Shell.Current.GoToAsync("//PartidosPage");
        }
    }

    private async void OnLoginClicked(object? sender, EventArgs e)
    {
        if (_isLoading) return;

        var username = UsernameEntry.Text?.Trim();
        var password = PasswordEntry.Text;

        if (string.IsNullOrEmpty(username))
        {
            ShowError("El usuario es requerido.");
            return;
        }

        if (string.IsNullOrEmpty(password))
        {
            ShowError("La clave es requerida.");
            return;
        }

        HideError();
        SetLoading(true);

        try
        {
            var response = await _ticketService.LoginAsync(username, password);

            if (response.Exito)
            {
                _sesion.Login(response.UsuarioId, username);
                await Shell.Current.GoToAsync("//PartidosPage");
            }
            else
            {
                ShowError(response.Mensaje ?? "Credenciales invalidas.");
            }
        }
        catch (Exception ex)
        {
            ShowError($"Error de conexion: {ex.GetType().Name}: {ex.Message}");
        }
        finally
        {
            SetLoading(false);
        }
    }

    private void SetLoading(bool loading)
    {
        _isLoading = loading;
        LoginButton.IsEnabled = !loading;
        UsernameEntry.IsEnabled = !loading;
        PasswordEntry.IsEnabled = !loading;

        if (loading)
        {
            LoginButton.Text = "";
            LoadingStack.IsVisible = true;
        }
        else
        {
            LoginButton.Text = "Ingresar";
            LoadingStack.IsVisible = false;
        }
    }

    private void ShowError(string message)
    {
        ErrorLabel.Text = message;
        ErrorFrame.IsVisible = true;
    }

    private void HideError()
    {
        ErrorFrame.IsVisible = false;
    }
}
