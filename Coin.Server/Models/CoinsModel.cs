using System.Text.Json.Serialization;

namespace Coin.Server.Models;

public class CoinData
{
    public int? Id { get; set; }
    public string? Name { get; set; }
}

public class MessageContent
{
    public string Msg { get; set; }
    public string Status { get; set; }
}

public class CryptocurrencyTradingModel
{
    public string? Id { get; set; }
    public int? CoinId_1 { get; set; }
    public decimal? CoinPrice_1 { get; set; }
    public decimal? CoinUnit_1 { get; set; }
    public int? CoinId_2 { get; set; }
    public decimal? CoinPrice_2 { get; set; }
    public decimal? CoinUnit_2 { get; set; }
    public string? TradingDate { get; set; }
    public string? AssetRestructuring { get; set; }
    public decimal? AssetRestructuringUnit { get; set; }
    public string? AssetRestructuringDate { get; set; }
    public decimal? ProfitPercent { get; set; }
    public decimal? Profit { get; set; }
    public string? Memo { get; set; }
}