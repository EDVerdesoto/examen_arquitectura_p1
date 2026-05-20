using CLIESC.Controlador;
using CLIESC.Modelo;
using TicketPremiumServiceRef;

namespace CLIESC.Vistas;

[QueryProperty(nameof(CodigoPartido), "CodigoPartido")]
[QueryProperty(nameof(CodigoLocalidad), "CodigoLocalidad")]
public partial class ComprarPage : ContentPage
{
    private readonly TicketService _ticketService;
    private readonly Sesion _sesion;

    private int _codigoPartido;
    public int CodigoPartido
    {
        get => _codigoPartido;
        set => _codigoPartido = value;
    }

    private string _codigoLocalidad = string.Empty;
    public string CodigoLocalidad
    {
        get => _codigoLocalidad;
        set => _codigoLocalidad = value;
    }

    private LocalidadDTO? _localidad;
    private PartidoDTO? _partido;
    private CompraResponse? _compraExitosa;
    private bool _isComprando;
    private bool _isLoaded;

    public ComprarPage(TicketService ticketService, Sesion sesion)
    {
        InitializeComponent();
        _ticketService = ticketService;
        _sesion = sesion;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (!_sesion.IsAuthenticated)
        {
            await Shell.Current.GoToAsync("//LoginPage");
            return;
        }
        CodigoLocalidad = Uri.UnescapeDataString(CodigoLocalidad);

        if (_compraExitosa != null) return;
        if (_isLoaded) return;
        _isLoaded = true;
        await LoadLocalidad();
    }

    private async Task LoadLocalidad()
    {
        SetLoading(true);
        ErrorFrame.IsVisible = false;
        ContentGrid.IsVisible = false;
        CompraFormCard.IsVisible = false;
        SuccessCard.IsVisible = false;

        try
        {
            var partidos = await _ticketService.ObtenerPartidosDisponiblesAsync();
            _partido = partidos.FirstOrDefault(p => p.Codigo == CodigoPartido);

            if (_partido != null)
            {
                HeaderSubtitle.Text = $"{_partido.EquipoLocal} vs {_partido.EquipoVisita} — {_partido.Fecha:dd/MM/yyyy HH:mm} — {_partido.Lugar}";
            }

            var localidades = await _ticketService.ObtenerLocalidadesPorPartidoAsync(CodigoPartido);
            _localidad = localidades.FirstOrDefault(l => l.CodigoLocalidad == CodigoLocalidad);

            if (_localidad == null)
            {
                ErrorLabel.Text = "Localidad no encontrada.";
                ErrorFrame.IsVisible = true;
                return;
            }

            if (_localidad.Disponibilidad == 0)
            {
                ErrorLabel.Text = "Esta localidad ya no tiene boletos disponibles.";
                ErrorFrame.IsVisible = true;
                return;
            }

            LocalidadNameLabel.Text = _localidad.CodigoLocalidad;
            PrecioUnitarioLabel.Text = $"${_localidad.Precio:N2}";
            DisponiblesLabel.Text = _localidad.Disponibilidad.ToString();
            MaxLabel.Text = $"Maximo {_localidad.Disponibilidad} boleto(s) disponible(s).";
            PrecioDesglose.IsVisible = false;

            CompraFormCard.IsVisible = true;
            ContentGrid.IsVisible = true;
        }
        catch (Exception ex)
        {
            ErrorLabel.Text = $"Error al cargar la informacion de la localidad: {ex.GetType().Name}: {ex.Message}";
            ErrorFrame.IsVisible = true;
        }
        finally
        {
            SetLoading(false);
        }
    }

    private void OnCantidadChanged(object? sender, TextChangedEventArgs e)
    {
        if (_localidad == null) return;

        if (int.TryParse(CantidadEntry.Text, out int cantidad) && cantidad > 0)
        {
            PrecioDesglose.IsVisible = true;
            var subtotal = cantidad * _localidad.Precio;
            var iva = subtotal * 0.15m;
            var total = subtotal + iva;

            SubtotalLabel.Text = $"${subtotal:N2}";
            IvaLabel.Text = $"${iva:N2}";
            TotalLabel.Text = $"${total:N2}";
        }
        else
        {
            PrecioDesglose.IsVisible = false;
        }
    }

    private async void OnComprarClicked(object? sender, EventArgs e)
    {
        if (_isComprando || _localidad == null) return;

        if (!int.TryParse(CantidadEntry.Text, out int cantidad) || cantidad < 1)
        {
            ErrorLabel.Text = "La cantidad debe ser al menos 1.";
            ErrorFrame.IsVisible = true;
            return;
        }

        if (cantidad > _localidad.Disponibilidad)
        {
            ErrorLabel.Text = $"Solo hay {_localidad.Disponibilidad} boleto(s) disponible(s).";
            ErrorFrame.IsVisible = true;
            return;
        }

        ErrorFrame.IsVisible = false;
        _isComprando = true;
        ConfirmButton.Text = "";
        ConfirmButton.IsEnabled = false;

        try
        {
            var request = new CompraRequest
            {
                UsuarioId = _sesion.UsuarioId,
                CodigoPartido = CodigoPartido,
                CodigoLocalidad = _localidad.CodigoLocalidad,
                Cantidad = cantidad,
                PrecioUnitario = _localidad.Precio,
                PartidoDescripcion = $"Partido #{CodigoPartido} - {CodigoLocalidad}",
                FechaPartido = DateTime.Now
            };

            var response = await _ticketService.ComprarBoletosAsync(request);

            if (response.Exito)
            {
                _compraExitosa = response;
                ShowSuccess(response, cantidad);
            }
            else
            {
                ErrorLabel.Text = response.Mensaje ?? "La compra no pudo ser procesada.";
                ErrorFrame.IsVisible = true;
            }
        }
        catch (Exception ex)
        {
            ErrorLabel.Text = $"Error al procesar la compra: {ex.GetType().Name}: {ex.Message}";
            ErrorFrame.IsVisible = true;
        }
        finally
        {
            _isComprando = false;
            ConfirmButton.Text = "Confirmar compra";
            ConfirmButton.IsEnabled = true;
        }
    }

    private void ShowSuccess(CompraResponse response, int cantidad)
    {
        CompraFormCard.IsVisible = false;
        SuccessCard.IsVisible = true;

        SuccessMessage.Text = response.Mensaje ?? "Compra realizada exitosamente.";
        FacturaIdLabel.Text = response.FacturaId.ToString();
        FacturaLocalidadLabel.Text = _localidad?.CodigoLocalidad ?? CodigoLocalidad;
        FacturaCantidadLabel.Text = cantidad.ToString();
        FacturaPrecioLabel.Text = _localidad != null ? $"${_localidad.Precio:N2}" : "$0.00";
        FacturaSubtotalLabel.Text = $"${response.Subtotal:N2}";
        FacturaIvaLabel.Text = $"${response.Iva:N2}";
        FacturaTotalLabel.Text = $"${response.Total:N2}";
    }

    private async void OnVerMasPartidosClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//PartidosPage");
    }

    private async void OnNuevaCompraClicked(object? sender, EventArgs e)
    {
        _compraExitosa = null;
        _isLoaded = false;
        CantidadEntry.Text = "1";
        PrecioDesglose.IsVisible = false;
        await LoadLocalidad();
    }

    private async void OnBackToLocalidadesClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }

    private void SetLoading(bool loading)
    {
        LoadingStack.IsVisible = loading;
    }
}
