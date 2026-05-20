using CLIESC.Controlador;
using TicketPremiumServiceRef;

namespace CLIESC.Vistas;

[QueryProperty(nameof(CodigoPartido), "CodigoPartido")]
public partial class LocalidadesPage : ContentPage
{
    private readonly TicketService _ticketService;
    private readonly Sesion _sesion;
    private bool _isLoaded;

    private int _codigoPartido;
    public int CodigoPartido
    {
        get => _codigoPartido;
        set
        {
            if (_codigoPartido != value)
            {
                _codigoPartido = value;
                _isLoaded = false;
            }
        }
    }

    private PartidoDTO? _partido;

    public LocalidadesPage(TicketService ticketService, Sesion sesion)
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
        await LoadLocalidades();
    }

    private async Task LoadLocalidades()
    {
        SetLoading(true);
        ErrorFrame.IsVisible = false;
        EmptyFrame.IsVisible = false;
        LocalidadesGrid.IsVisible = false;

        try
        {
            var partidos = await _ticketService.ObtenerPartidosDisponiblesAsync();
            _partido = partidos.FirstOrDefault(p => p.Codigo == CodigoPartido);

            if (_partido != null)
            {
                HeaderSubtitle.Text = $"{_partido.EquipoLocal} vs {_partido.EquipoVisita} — {_partido.Fecha:dd/MM/yyyy HH:mm} — {_partido.Lugar}";
            }

            var localidades = await _ticketService.ObtenerLocalidadesPorPartidoAsync(CodigoPartido);

            if (localidades.Length == 0)
            {
                EmptyFrame.IsVisible = true;
                return;
            }

            LocalidadesGrid.Children.Clear();
            foreach (var loc in localidades)
            {
                LocalidadesGrid.Children.Add(CreateLocalidadCard(loc));
            }
            LocalidadesGrid.IsVisible = true;
        }
        catch (Exception ex)
        {
            ErrorLabel.Text = $"No se pudieron cargar las localidades: {ex.GetType().Name}: {ex.Message}";
            ErrorFrame.IsVisible = true;
        }
        finally
        {
            SetLoading(false);
        }
    }

    private View CreateLocalidadCard(LocalidadDTO loc)
    {
        var card = new Frame
        {
            Style = (Style)Application.Current!.Resources["LocalidadCard"],
            WidthRequest = 260,
            Margin = new Thickness(6)
        };

        if (loc.Disponibilidad == 0)
            card.Opacity = 0.7;

        var stack = new VerticalStackLayout { Spacing = 8, Padding = new Thickness(16) };

        var nameLabel = new Label
        {
            Text = loc.CodigoLocalidad,
            FontFamily = "OpenSansSemibold",
            FontSize = 15,
            TextColor = (Color)Application.Current!.Resources["TextDark"]
        };
        stack.Children.Add(nameLabel);

        var badge = new Frame
        {
            HasShadow = false,
            CornerRadius = 6,
            Padding = new Thickness(8, 4),
            HorizontalOptions = LayoutOptions.Start
        };

        var badgeLabel = new Label
        {
            FontSize = 12,
            FontFamily = "OpenSansSemibold"
        };

        if (loc.Disponibilidad > 0)
        {
            badge.BackgroundColor = Color.FromArgb("#1A0F4C5C");
            badge.BorderColor = (Color)Application.Current!.Resources["Primary"];
            badgeLabel.Text = $"{loc.Disponibilidad} disponibles";
            badgeLabel.TextColor = (Color)Application.Current!.Resources["Primary"];
        }
        else
        {
            badge.BackgroundColor = Color.FromArgb("#1ADC3545");
            badge.BorderColor = (Color)Application.Current!.Resources["Danger"];
            badgeLabel.Text = "Agotado";
            badgeLabel.TextColor = (Color)Application.Current!.Resources["Danger"];
        }

        badge.Content = badgeLabel;
        stack.Children.Add(badge);

        var priceLabel = new Label
        {
            Text = $"${loc.Precio:N2}",
            Style = (Style)Application.Current!.Resources["PriceLabel"]
        };
        stack.Children.Add(priceLabel);

        var subLabel = new Label
        {
            Text = "por boleto",
            Style = (Style)Application.Current!.Resources["MutedLabel"]
        };
        stack.Children.Add(subLabel);

        if (loc.Disponibilidad > 0)
        {
            var button = new Button
            {
                Text = "Comprar",
                Style = (Style)Application.Current!.Resources["CardActionButton"],
                Margin = new Thickness(0, 4, 0, 0)
            };
            button.Clicked += async (s, e) =>
            {
                await Shell.Current.GoToAsync($"ComprarPage?CodigoPartido={CodigoPartido}&CodigoLocalidad={Uri.EscapeDataString(loc.CodigoLocalidad)}");
            };
            stack.Children.Add(button);
        }
        else
        {
            var button = new Button
            {
                Text = "Agotado",
                BackgroundColor = Color.FromArgb("#999"),
                TextColor = Colors.White,
                FontFamily = "OpenSansSemibold",
                FontSize = 14,
                CornerRadius = 8,
                Padding = new Thickness(28, 10),
                IsEnabled = false,
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(0, 4, 0, 0)
            };
            stack.Children.Add(button);
        }

        card.Content = stack;
        return card;
    }

    private void SetLoading(bool loading)
    {
        LoadingStack.IsVisible = loading;
    }

    private async void OnResumenVentasClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"ResumenVentasPage?CodigoPartido={CodigoPartido}");
    }
}
