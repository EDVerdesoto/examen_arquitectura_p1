using CLIESC.Controlador;
using TicketPremiumServiceRef;

namespace CLIESC.Vistas;

[QueryProperty(nameof(CodigoPartido), "CodigoPartido")]
public partial class ResumenVentasPage : ContentPage
{
    private readonly TicketService _ticketService;
    private readonly Sesion _sesion;
    private bool _isLoaded;

    private int _codigoPartido;
    public int CodigoPartido
    {
        get => _codigoPartido;
        set => _codigoPartido = value;
    }

    public ResumenVentasPage(TicketService ticketService, Sesion sesion)
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
        if (_isLoaded) return;
        _isLoaded = true;
        await LoadResumen();
    }

    private async Task LoadResumen()
    {
        SetLoading(true);
        ErrorFrame.IsVisible = false;
        EmptyFrame.IsVisible = false;
        TableFrame.IsVisible = false;

        try
        {
            var resumen = await _ticketService.ObtenerResumenVentasAsync(CodigoPartido);

            HeaderSubtitle.Text = $"{resumen.Partido} — {resumen.Fecha:dd/MM/yyyy HH:mm} — #{CodigoPartido}";

            if (resumen.Detalles.Length == 0)
            {
                EmptyFrame.IsVisible = true;
                return;
            }

            RowsStack.Children.Clear();
            var isAlternate = false;
            foreach (var detalle in resumen.Detalles)
            {
                RowsStack.Children.Add(CreateRow(detalle, isAlternate));
                isAlternate = !isAlternate;
            }

            FooterVendidos.Text = resumen.Detalles.Sum(d => d.Vendidos).ToString();
            FooterRecaudado.Text = $"${resumen.Detalles.Sum(d => d.TotalRecaudado):N2}";

            TableFrame.IsVisible = true;
        }
        catch (Exception ex)
        {
            ErrorLabel.Text = $"No se pudo cargar el resumen de ventas: {ex.GetType().Name}: {ex.Message}";
            ErrorFrame.IsVisible = true;
        }
        finally
        {
            SetLoading(false);
        }
    }

    private View CreateRow(ResumenVentaDetalleDTO detalle, bool alternate)
    {
        var grid = new Grid
        {
            ColumnDefinitions =
            {
                new ColumnDefinition(new GridLength(2, GridUnitType.Star)),
                new ColumnDefinition(new GridLength(1, GridUnitType.Star)),
                new ColumnDefinition(new GridLength(1, GridUnitType.Star))
            },
            Padding = new Thickness(20, 14),
            BackgroundColor = alternate ? Color.FromArgb("#F8FAFC") : Colors.White
        };

        var localidadLabel = new Label
        {
            Text = detalle.Localidad,
            FontSize = 13,
            TextColor = (Color)Application.Current!.Resources["TextDark"]
        };
        Grid.SetColumn(localidadLabel, 0);

        var vendidosLabel = new Label
        {
            Text = detalle.Vendidos.ToString(),
            FontSize = 13,
            FontFamily = "OpenSansSemibold",
            TextColor = (Color)Application.Current!.Resources["Primary"],
            HorizontalTextAlignment = TextAlignment.End
        };
        Grid.SetColumn(vendidosLabel, 1);

        var recaudadoLabel = new Label
        {
            Text = $"${detalle.TotalRecaudado:N2}",
            FontSize = 13,
            FontFamily = "OpenSansSemibold",
            TextColor = (Color)Application.Current!.Resources["TextDark"],
            HorizontalTextAlignment = TextAlignment.End
        };
        Grid.SetColumn(recaudadoLabel, 2);

        grid.Children.Add(localidadLabel);
        grid.Children.Add(vendidosLabel);
        grid.Children.Add(recaudadoLabel);

        return grid;
    }

    private void SetLoading(bool loading)
    {
        LoadingStack.IsVisible = loading;
    }

    private async void OnLocalidadesClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"LocalidadesPage?CodigoPartido={CodigoPartido}");
    }

    private async void OnResumenClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("ResumenPage");
    }
}
