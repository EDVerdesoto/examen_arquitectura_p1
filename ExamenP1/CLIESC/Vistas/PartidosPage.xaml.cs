using CLIESC.Controlador;
using TicketPremiumServiceRef;

namespace CLIESC.Vistas;

public partial class PartidosPage : ContentPage
{
    private readonly TicketService _ticketService;
    private readonly Sesion _sesion;
    private bool _isLoaded;

    public PartidosPage(TicketService ticketService, Sesion sesion)
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
        await LoadPartidos();
    }

    private async Task LoadPartidos()
    {
        SetLoading(true);
        ErrorFrame.IsVisible = false;
        EmptyFrame.IsVisible = false;
        PartidosGrid.IsVisible = false;

        try
        {
            var partidos = await _ticketService.ObtenerPartidosDisponiblesAsync();

            if (partidos.Length == 0)
            {
                EmptyFrame.IsVisible = true;
                return;
            }

            PartidosGrid.Children.Clear();
            foreach (var partido in partidos)
            {
                PartidosGrid.Children.Add(CreatePartidoCard(partido));
            }
            PartidosGrid.IsVisible = true;
        }
        catch (Exception ex)
        {
            ErrorLabel.Text = $"No se pudieron cargar los partidos: {ex.GetType().Name}: {ex.Message}";
            ErrorFrame.IsVisible = true;
        }
        finally
        {
            SetLoading(false);
        }
    }

    private View CreatePartidoCard(PartidoDTO partido)
    {
        var card = new Frame
        {
            Style = (Style)Application.Current!.Resources["MatchCard"],
            WidthRequest = 330
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
            HorizontalOptions = LayoutOptions.Center,
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

        var button = new Button
        {
            Text = "Ver localidades",
            Style = (Style)Application.Current!.Resources["CardActionButton"],
            Margin = new Thickness(0, 8, 0, 0)
        };
        button.Clicked += async (s, e) =>
        {
            await Shell.Current.GoToAsync($"LocalidadesPage?CodigoPartido={partido.Codigo}");
        };
        stack.Children.Add(button);

        card.Content = stack;
        return card;
    }

    private void SetLoading(bool loading)
    {
        LoadingStack.IsVisible = loading;
    }

    private async void OnLogoutClicked(object? sender, EventArgs e)
    {
        _sesion.Logout();
        await Shell.Current.GoToAsync("//LoginPage");
    }

    private async void OnResumenClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("ResumenPage");
    }
}
