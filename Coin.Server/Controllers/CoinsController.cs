﻿using Coin.Server.Appcode;
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
                MessageContent mc = new MessageContent()
                {
                    Msg = "Coin added",
                    Status = "Success"
                };
                cr.StatusCode = 200;
                cr.Content = JsonSerializer.Serialize(mc);
            }
            else
            {
                MessageContent mc = new MessageContent()
                {
                    Msg = "Server error, coin not added",
                    Status = "Error"
                };
                cr.StatusCode = 400;
                cr.Content = JsonSerializer.Serialize(mc);
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
                MessageContent mc = new MessageContent()
                {
                    Msg = "Coin edited",
                    Status = "Success"
                };
                cr.StatusCode = 200;
                cr.Content = JsonSerializer.Serialize(mc);
            }
            else
            {
                MessageContent mc = new MessageContent()
                {
                    Msg = "Server error, coin not edited",
                    Status = "Error"
                };
                cr.StatusCode = 400;
                cr.Content = JsonSerializer.Serialize(mc);
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
    public async Task<IActionResult> DeleteCoin([FromBody] CoinData coin)
    {
        ContentResult cr = new ContentResult();
        _logger.LogInformation("DeleteCoin called");
        try
        {
            if (await _dbHelper.DeleteCoin(coin.Id))
            {
                MessageContent mc = new MessageContent()
                {
                    Msg = "Coin deleted",
                    Status = "Success"
                };
                cr.StatusCode = 200;
                cr.Content = JsonSerializer.Serialize(mc);
            }
            else
            {
                MessageContent mc = new MessageContent()
                {
                    Msg = "Server error, coin not deleted",
                    Status = "Error"
                };
                cr.StatusCode = 400;
                cr.Content = JsonSerializer.Serialize(mc);
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
