using Coin.Server.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Coin.Server.Controllers;

[Route("[controller]")]
[ApiController]
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
                new CoinData { Id = 1, Name = "Bitcoin", Symbol = "BTC" },
                new CoinData { Id = 2, Name = "Ethereum", Symbol = "ETH" }
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
