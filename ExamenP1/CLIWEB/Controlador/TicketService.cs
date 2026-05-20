using TicketPermiumServiceRef;

namespace CLIWEB.Controlador;

public class TicketService
{
    private readonly TicketPremiumServiceClient _client;

    public TicketService(TicketPremiumServiceClient client)
    {
        _client = client;
    }

    public async Task<LoginResponse> LoginAsync(string nombreUsuario, string clave)
    {
        return await _client.LoginAsync(nombreUsuario, clave);
    }

    public async Task<PartidoDTO[]> ObtenerPartidosDisponiblesAsync()
    {
        return await _client.ObtenerPartidosDisponiblesAsync();
    }

    public async Task<LocalidadDTO[]> ObtenerLocalidadesPorPartidoAsync(int codigoPartido)
    {
        return await _client.ObtenerLocalidadesPorPartidoAsync(codigoPartido);
    }

    public async Task<CompraResponse> ComprarBoletosAsync(CompraRequest request)
    {
        return await _client.ComprarBoletosAsync(request);
    }

    public async Task<ResumenVentaDTO> ObtenerResumenVentasAsync(int codigoPartido)
    {
        return await _client.ObtenerResumenVentasAsync(codigoPartido);
    }
}
