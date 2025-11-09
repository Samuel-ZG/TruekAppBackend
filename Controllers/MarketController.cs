using Microsoft.AspNetCore.Mvc;
using TruekAppAPI.Services;
using TruekAppAPI.DTO.Market;
using System.Threading.Tasks;

[ApiController]
[Route("api/v1/market")]
public class MarketController : ControllerBase
{
    private readonly BinanceP2PService _binanceP2PService;

    public MarketController(BinanceP2PService binanceP2PService)
    {
        _binanceP2PService = binanceP2PService;
    }

    [HttpGet("usdt-bob")]
    public async Task<ActionResult<BinanceP2PPriceDto>> GetUSDTtoBOBPrice()
    {
        var precio = await _binanceP2PService.ObtenerPrecioUSDTaBOBAsync();

        if (precio == null)
            return NotFound("No se encontraron ofertas P2P para USDT a BOB");

        var dto = new BinanceP2PPriceDto
        {
            Price = precio.Value
        };

        return Ok(dto);
    }
}