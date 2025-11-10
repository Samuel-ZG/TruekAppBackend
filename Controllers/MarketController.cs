using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TruekAppAPI.Services;

namespace TruekAppAPI.Controllers
{
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
        public async Task<IActionResult> GetUSDTtoBOBPrice()
        {
            var precio = await _binanceP2PService.ObtenerPrecioUSDTaBOBAsync();

            if (precio == null)
                return NotFound("No se encontraron ofertas P2P para USDT a BOB");

            return Ok(new { price = precio });
        }
    }
}