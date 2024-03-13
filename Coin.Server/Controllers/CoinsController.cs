using Coin.Server.Appcode;
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
    private readonly DBHelper _dbHelper;

    public CoinsController(ILogger<CoinsController> logger, DBHelper dbHelper)
    {
        _logger = logger;
        _dbHelper = dbHelper;
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetCoins()
    {
        ContentResult cr = new ContentResult();
        _logger.LogInformation("GetCoins called");
        try
        {
            cr.StatusCode = 200;

            List<CoinData> Coins = await _dbHelper.GetCoins();

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

    [HttpPost("[action]")]
    public async Task<IActionResult> AddCoin([FromBody] CoinData coin)
    {
        coin.SanitizeStringProperties();
        ContentResult cr = new ContentResult();
        _logger.LogInformation("AddCoin called");
        try
        {
            if (await _dbHelper.AddCoin(coin))
            {
                cr.StatusCode = 200;
                cr.Content = "{\"Msg\":\"Coin added\"}";
            }
            else
            {
                cr.StatusCode = 400;
                cr.Content = "{\"Msg\":\"Server error, coin not added\"}";
            }
        }
        catch (Exception ex)
        {
            cr.StatusCode = 400;
            cr.Content = "Bad Request";
            _logger.LogError(ex, "AddCoin failed");
        }
        return cr;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> EditCoin([FromBody] CoinData coin)
    {
        coin.SanitizeStringProperties();
        ContentResult cr = new ContentResult();
        _logger.LogInformation("EditCoin called");
        try
        {
            if (await _dbHelper.EditCoin(coin))
            {
                cr.StatusCode = 200;
                cr.Content = "{\"Msg\":\"Coin edited\"}";
            }
            else
            {
                cr.StatusCode = 400;
                cr.Content = "{\"Msg\":\"Server error, coin not edited\"}";
            }
        }
        catch (Exception ex)
        {
            cr.StatusCode = 400;
            cr.Content = "Bad Request";
            _logger.LogError(ex, "EditCoin failed");
        }
        return cr;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> DeleteCoin([FromBody] int id)
    {
        ContentResult cr = new ContentResult();
        _logger.LogInformation("DeleteCoin called");
        try
        {
            if (await _dbHelper.DeleteCoin(id))
            {
                cr.StatusCode = 200;
                cr.Content = "{\"Msg\":\"Coin deleted\"}";
            }
            else
            {
                cr.StatusCode = 400;
                cr.Content = "{\"Msg\":\"Server error, coin not deleted\"}";
            }
        }
        catch (Exception ex)
        {
            cr.StatusCode = 400;
            cr.Content = "Bad Request";
            _logger.LogError(ex, "DeleteCoin failed");
        }
        return cr;
    }
}
