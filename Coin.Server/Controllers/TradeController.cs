using Coin.Server.Appcode;
using Coin.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Coin.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TradeController : ControllerBase
{
    private readonly ILogger<TradeController> _logger;
    private readonly DBHelper _dbHelper;

    public TradeController(ILogger<TradeController> logger,
                           DBHelper dbHelper)
    {
        _logger = logger;
        _dbHelper = dbHelper;
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetTradingDetails()
    {
        ContentResult cr = new ContentResult();
        _logger.LogInformation("GetTradingDetail called");
        try
        {
            cr.StatusCode = 200;

            List<CryptocurrencyTradingModel> tradingDetails = await _dbHelper.GetTradingDetail();

            cr.Content = JsonSerializer.Serialize(tradingDetails);
        }
        catch (Exception ex)
        {
            cr.StatusCode = 400;
            cr.Content = "Bad Request";
            _logger.LogError(ex, "GetTradingDetail failed");
        }
        return cr;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> TradingDetailAdd([FromBody] CryptocurrencyTradingModel tradingDetail)
    {
        tradingDetail.SanitizeStringProperties();
        ContentResult cr = new ContentResult();
        _logger.LogInformation("TradingDetailAdd called");
        try
        {
            if (await _dbHelper.TradingAdd(tradingDetail))
            {
                MessageContent mc = new MessageContent()
                {
                    Msg = "Trading detail added",
                    Status = "Success"
                };
                cr.StatusCode = 200;
                cr.Content = JsonSerializer.Serialize(mc);
            }
            else
            {
                cr.StatusCode = 400;
                cr.Content = "Bad Request";
            }
        }
        catch (Exception ex)
        {
            cr.StatusCode = 400;
            cr.Content = "Bad Request";
            _logger.LogError(ex, "TradingDetailAdd failed");
        }
        return cr;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> TradingDetailEdit([FromBody] CryptocurrencyTradingModel tradingDetail)
    {
        tradingDetail.SanitizeStringProperties();
        ContentResult cr = new ContentResult();
        _logger.LogInformation("TradingEdit called");
        try
        {
            if (await _dbHelper.TradingEdit(tradingDetail))
            {
                MessageContent mc = new MessageContent()
                {
                    Msg = "Trading detail edited",
                    Status = "Success"
                };
                cr.StatusCode = 200;
                cr.Content = JsonSerializer.Serialize(mc);
            }
            else
            {
                cr.StatusCode = 400;
                cr.Content = "Bad Request";
            }
        }
        catch (Exception ex)
        {
            cr.StatusCode = 400;
            cr.Content = "Bad Request";
            _logger.LogError(ex, "TradingEdit failed");
        }
        return cr;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> TradingDetailDelete([FromBody] CryptocurrencyTradingModel tradingDetail)
    {
        ContentResult cr = new ContentResult();
        _logger.LogInformation("TradingDetailDelete called");
        try
        {
            if (await _dbHelper.TradingDelete(tradingDetail.Id))
            {
                MessageContent mc = new MessageContent()
                {
                    Msg = "Trading detail deleted",
                    Status = "Success"
                };
                cr.StatusCode = 200;
                cr.Content = JsonSerializer.Serialize(mc);
            }
            else
            {
                cr.StatusCode = 400;
                cr.Content = "Bad Request";
            }
        }
        catch (Exception ex)
        {
            cr.StatusCode = 400;
            cr.Content = "Bad Request";
            _logger.LogError(ex, "TradingDetailDelete failed");
        }
        return cr;
    }
}
