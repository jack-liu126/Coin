using Coin.Server.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Coin.Server.Controllers;

[Route("[controller]")]
[ApiController]
[EnableCors("AllowSpecificOrigin")]
public class CoinsController : ControllerBase
{
    private readonly ILogger<CoinsController> _logger;

    public CoinsController(ILogger<CoinsController> logger)
    {
        _logger = logger;
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetCoins()
    {
        ContentResult cr = new ContentResult();
        _logger.LogInformation("GetCoins called");
        try
        {
            cr.StatusCode = 200;

            List<CoinData> Coins = new List<CoinData>
            {
                new CoinData { Id = 1, Name = "BTC"},
                new CoinData { Id = 2, Name = "ETH" },
                new CoinData { Id = 3, Name = "FTM" },
                new CoinData { Id = 4, Name = "SOL" },
                new CoinData { Id = 5, Name = "LUNA" },
                new CoinData { Id = 6, Name = "ORDI" },
                new CoinData { Id = 7, Name = "AI" },
                new CoinData { Id = 8, Name = "MATIC" },
                new CoinData { Id = 9, Name = "PROTAL" },
                new CoinData { Id = 10, Name = "WLD" },
                new CoinData { Id = 11, Name = "API3" },
            };

            cr.Content = JsonSerializer.Serialize(Coins);
        }
        catch (Exception ex)
        {
            cr.StatusCode = 400;
            cr.Content = "Bad Request";
            _logger.LogError(ex, "GetCoins failed");
        }
        return cr;
    }
}
