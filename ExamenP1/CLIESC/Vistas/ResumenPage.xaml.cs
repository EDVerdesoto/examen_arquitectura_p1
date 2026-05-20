using CLIESC.Controlador;
using TicketPremiumServiceRef;

namespace CLIESC.Vistas;

public partial class ResumenPage : ContentPage
{
    private readonly TicketService _ticketService;
    private readonly Sesion _sesion;
    private bool _isLoaded;

    public ResumenPage(TicketService ticketService, Sesion sesion)
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
        ResumenGrid.IsVisible = false;

        try
        {
            var partidos = await _ticketService.ObtenerPartidosDisponiblesAsync();
            var resumenes = new List<(PartidoDTO Partido, ResumenVentaDTO Resumen)>();

            foreach (var partido in partidos)
            {
                try
                {
                    var resumen = await _ticketService.ObtenerResumenVentasAsync(partido.Codigo);
                    resumenes.Add((partido, resumen));
                }
                catch { }
            }

            resumenes = resumenes.OrderByDescending(r => r.Partido.Fecha).ToList();

            if (resumenes.Count == 0)
            {
                EmptyFrame.IsVisible = true;
                return;
            }

            ResumenGrid.Children.Clear();
            foreach (var (partido, resumen) in resumenes)
            {
                ResumenGrid.Children.Add(CreateResumenCard(partido, resumen));
            }
            ResumenGrid.IsVisible = true;
        }
        catch (Exception ex)
        {
            ErrorLabel.Text = $"No se pudo cargar la informacion de ventas: {ex.GetType().Name}: {ex.Message}";
            ErrorFrame.IsVisible = true;
        }
        finally
        {
            SetLoading(false);
        }
    }

    private View CreateResumenCard(PartidoDTO partido, ResumenVentaDTO resumen)
    {
        var totalVendidos = resumen.Detalles.Sum(d => d.Vendidos);
        var totalRecaudado = resumen.Detalles.Sum(d => d.TotalRecaudado);

        var card = new Frame
        {
            Style = (Style)Application.Current!.Resources["MatchCard"],
            WidthRequest = 380
        };

        var stack = new VerticalStackLayout { Spacing = 8, Padding = new Thickness(16) };

        var teamsRow = new Grid
        {
            ColumnDefinitions =
            {
                new ColumnDefinition(GridLength.Star),
                new ColumnDefinition(GridLength.Auto),
                new ColumnDefinition(GridLength.Star)
            }
        };

        var localLabel = new Label
        {
            Text = partido.EquipoLocal,
            Style = (Style)Application.Current!.Resources["TeamName"],
            HorizontalTextAlignment = TextAlignment.End
        };
        Grid.SetColumn(localLabel, 0);

        var vsLabel = new Label
        {
            Text = "VS",
            TextColor = Color.FromArgb("#AAA"),
            FontSize = 12,
            VerticalOptions = LayoutOptions.Center,
            Margin = new Thickness(12, 0)
        };
        Grid.SetColumn(vsLabel, 1);

        var visitaLabel = new Label
        {
            Text = partido.EquipoVisita,
            Style = (Style)Application.Current!.Resources["TeamName"],
            HorizontalTextAlignment = TextAlignment.Start
        };
        Grid.SetColumn(visitaLabel, 2);

        teamsRow.Children.Add(localLabel);
        teamsRow.Children.Add(vsLabel);
        teamsRow.Children.Add(visitaLabel);
        stack.Children.Add(teamsRow);

        var divider = new BoxView
        {
            HeightRequest = 1,
            Color = Color.FromArgb("#F0F0F0"),
            Margin = new Thickness(0, 4)
        };
        stack.Children.Add(divider);

        var infoGrid = new Grid
        {
            ColumnDefinitions =
            {
                new ColumnDefinition(GridLength.Star),
                new ColumnDefinition(GridLength.Star)
            },
            Margin = new Thickness(0, 4)
        };

        var dateLabel = new Label
        {
            Text = partido.Fecha.ToString("dd/MM/yyyy HH:mm"),
            FontSize = 13,
            TextColor = (Color)Application.Current!.Resources["TextDark"]
        };
        Grid.SetColumn(dateLabel, 0);

        var lugarLabel = new Label
        {
            Text = partido.Lugar,
            FontSize = 13,
            TextColor = (Color)Application.Current!.Resources["TextDark"]
        };
        Grid.SetColumn(lugarLabel, 1);

        infoGrid.Children.Add(dateLabel);
        infoGrid.Children.Add(lugarLabel);
        stack.Children.Add(infoGrid);

        var statsGrid = new Grid
        {
            ColumnDefinitions =
            {
                new ColumnDefinition(GridLength.Star),
                new ColumnDefinition(GridLength.Star)
            },
            Margin = new Thickness(0, 4)
        };

        var vendidosStack = new VerticalStackLayout
        {
            HorizontalOptions = LayoutOptions.Center,
            Spacing = 2
        };
        vendidosStack.Children.Add(new Label
        {
            Text = totalVendidos.ToString(),
            FontSize = 18,
            FontFamily = "OpenSansSemibold",
            TextColor = (Color)Application.Current!.Resources["Primary"],
            HorizontalTextAlignment = TextAlignment.Center
        });
        vendidosStack.Children.Add(new Label
        {
            Text = "Vendidos",
            FontSize = 11,
            TextColor = (Color)Application.Current!.Resources["TextMuted"],
            HorizontalTextAlignment = TextAlignment.Center
        });
        Grid.SetColumn(vendidosStack, 0);

        var recaudadoStack = new VerticalStackLayout
        {
            HorizontalOptions = LayoutOptions.Center,
            Spacing = 2
        };
        recaudadoStack.Children.Add(new Label
        {
            Text = $"${totalRecaudado:N0}",
            FontSize = 18,
            FontFamily = "OpenSansSemibold",
            TextColor = (Color)Application.Current!.Resources["Success"],
            HorizontalTextAlignment = TextAlignment.Center
        });
        recaudadoStack.Children.Add(new Label
        {
            Text = "Recaudado",
            FontSize = 11,
            TextColor = (Color)Application.Current!.Resources["TextMuted"],
            HorizontalTextAlignment = TextAlignment.Center
        });
        Grid.SetColumn(recaudadoStack, 1);

        statsGrid.Children.Add(vendidosStack);
        statsGrid.Children.Add(recaudadoStack);
        stack.Children.Add(statsGrid);

        var button = new Button
        {
            Text = "Ver detalle",
            Style = (Style)Application.Current!.Resources["CardActionButton"],
            Margin = new Thickness(0, 4, 0, 0)
        };
        button.Clicked += async (s, e) =>
        {
            await Shell.Current.GoToAsync($"ResumenVentasPage?CodigoPartido={partido.Codigo}");
        };
        stack.Children.Add(button);

        card.Content = stack;
        return card;
    }

    private void SetLoading(bool loading)
    {
        LoadingStack.IsVisible = loading;
    }

    private async void OnVolverPartidosClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//PartidosPage");
    }

    private async void OnBackClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}
