using Coin.Server.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Text;

namespace Coin.Server.Appcode;

public class DBHelper
{
    private readonly ILogger<DBHelper> _logger;
    private readonly SqlConnection _conn;

    public DBHelper(ILogger<DBHelper> logger, SqlConnection conn)
    {
        _logger = logger;
        _conn = conn;
    }

    public async Task<List<CoinData>> GetCoins()
    {
        List<CoinData> Coins = new List<CoinData>();
        try
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select Id,Name ");
            sql.AppendLine("from CoinType ");

            Coins = (await _conn.QueryAsync<CoinData>(sql.ToString())).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetCoins failed");
        }
        return Coins;
    }

    public async Task<bool> AddCoin(CoinData coin)
    {
        bool success = false;
        try
        {
            if (coin.Name != null)
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("insert into CoinType(Name) ");
                sql.AppendLine("values(@Name) ");
                DynamicParameters dp = new DynamicParameters();
                dp.Add("@Name", coin.Name);

                int result = await _conn.ExecuteAsync(sql.ToString(), dp);
                success = result > 0;
            }
            else
            {
                _logger.LogError("AddCoin failed, id or name is null");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "AddCoin failed");
        }
        return success;
    }

    public async Task<bool> EditCoin(CoinData coin)
    {
        bool success = false;
        try
        {
            if (coin.Id != null && coin.Name != null)
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("update CoinType ");
                sql.AppendLine("set Name = @Name ");
                sql.AppendLine("where Id = @Id ");
                DynamicParameters dp = new DynamicParameters();
                dp.Add("@Id", coin.Id);
                dp.Add("@Name", coin.Name);

                int result = await _conn.ExecuteAsync(sql.ToString(), dp);
                success = result > 0;
            }
            else
            {
                _logger.LogError("EditCoin failed, id or name is null");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "EditCoin failed");
        }
        return success;
    }

    public async Task<bool> DeleteCoin(int? id)
    {
        bool success = false;
        try
        {
            if (id != null)
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("delete from CoinType ");
                sql.AppendLine("where Id = @Id ");
                DynamicParameters dp = new DynamicParameters();
                dp.Add("@Id", id);

                int result = await _conn.ExecuteAsync(sql.ToString(), dp);
                success = result > 0;
            }
            else
            {
                _logger.LogError("DeleteCoin failed, id is null");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "DeleteCoin failed");
        }
        return success;
    }

    public async Task<List<CryptocurrencyTradingModel>> GetTradingDetail()
    {
        List<CryptocurrencyTradingModel> tradingDetails = new List<CryptocurrencyTradingModel>();
        try
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select Id,CoinId_1,CoinPrice_1,CoinUnit_1,CoinId_2,CoinPrice_2,CoinUnit_2,TradingDate,AssetRestructuring,AssetRestructuringUnit,AssetRestructuringDate,ProfitPercent,Profit,Memo ");
            sql.AppendLine("from CryptocurrencyTrading ");
            sql.AppendLine("where AssetRestructuring != 'false' ");

            tradingDetails = (await _conn.QueryAsync<CryptocurrencyTradingModel>(sql.ToString())).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetTradingDetail failed");
        }
        return tradingDetails;
    }

    public async Task<bool> TradingAdd(CryptocurrencyTradingModel trading)
    {
        bool success = false;
        try
        {
            if (trading.CoinId_1 != null && trading.CoinPrice_1 != null && trading.CoinUnit_1 != null && trading.CoinId_2 != null && trading.CoinPrice_2 != null && trading.CoinUnit_2 != null && trading.TradingDate != null)
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("insert into CryptocurrencyTrading(CoinId_1,CoinPrice_1,CoinUnit_1,CoinId_2,CoinPrice_2,CoinUnit_2,TradingDate,AssetRestructuring,AssetRestructuringUnit,AssetRestructuringDate,ProfitPercent,Profit,Memo) ");
                sql.AppendLine("values(@CoinId_1,@CoinPrice_1,@CoinUnit_1,@CoinId_2,@CoinPrice_2,@CoinUnit_2,@TradingDate,@AssetRestructuring,@AssetRestructuringUnit,@AssetRestructuringDate,@ProfitPercent,@Profit,@Memo) ");
                DynamicParameters dp = new DynamicParameters();
                dp.Add("@CoinId_1", trading.CoinId_1);
                dp.Add("@CoinPrice_1", trading.CoinPrice_1);
                dp.Add("@CoinUnit_1", trading.CoinUnit_1);
                dp.Add("@CoinId_2", trading.CoinId_2);
                dp.Add("@CoinPrice_2", trading.CoinPrice_2);
                dp.Add("@CoinUnit_2", trading.CoinUnit_2);
                dp.Add("@TradingDate", trading.TradingDate);
                dp.Add("@AssetRestructuring", trading.AssetRestructuring);
                dp.Add("@AssetRestructuringUnit", trading.AssetRestructuringUnit);
                dp.Add("@AssetRestructuringDate", trading.AssetRestructuringDate);
                dp.Add("@ProfitPercent", trading.ProfitPercent);
                dp.Add("@Profit", trading.Profit);
                dp.Add("@Memo", trading.Memo);

                int result = await _conn.ExecuteAsync(sql.ToString(), dp);
                success = result > 0;
            }
            else
            {
                _logger.LogError("TradingAdd failed, some value is null");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "TradingAdd failed");
        }
        return success;
    }

    public async Task<bool> TradingEdit(CryptocurrencyTradingModel trading)
    {
        bool success = false;
        try
        {
            if (trading.Id != null && trading.CoinId_1 != null && trading.CoinPrice_1 != null && trading.CoinUnit_1 != null && trading.CoinId_2 != null && trading.CoinPrice_2 != null && trading.CoinUnit_2 != null && trading.TradingDate != null)
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("update CryptocurrencyTrading ");
                sql.AppendLine("set CoinId_1 = @CoinId_1,CoinPrice_1 = @CoinPrice_1,CoinUnit_1 = @CoinUnit_1,CoinId_2 = @CoinId_2,CoinPrice_2 = @CoinPrice_2,CoinUnit_2 = @CoinUnit_2,TradingDate = @TradingDate,AssetRestructuring = @AssetRestructuring,AssetRestructuringUnit = @AssetRestructuringUnit,AssetRestructuringDate = @AssetRestructuringDate,ProfitPercent = @ProfitPercent,Profit = @Profit,Memo = @Memo ");
                sql.AppendLine("where Id = @Id ");
                DynamicParameters dp = new DynamicParameters();
                dp.Add("@Id", trading.Id);
                dp.Add("@CoinId_1", trading.CoinId_1);
                dp.Add("@CoinPrice_1", trading.CoinPrice_1);
                dp.Add("@CoinUnit_1", trading.CoinUnit_1);
                dp.Add("@CoinId_2", trading.CoinId_2);
                dp.Add("@CoinPrice_2", trading.CoinPrice_2);
                dp.Add("@CoinUnit_2", trading.CoinUnit_2);
                dp.Add("@TradingDate", trading.TradingDate);
                dp.Add("@AssetRestructuring", trading.AssetRestructuring);
                dp.Add("@AssetRestructuringUnit", trading.AssetRestructuringUnit);
                dp.Add("@AssetRestructuringDate", trading.AssetRestructuringDate);
                dp.Add("@ProfitPercent", trading.ProfitPercent);
                dp.Add("@Profit", trading.Profit);
                dp.Add("@Memo", trading.Memo);

                int result = await _conn.ExecuteAsync(sql.ToString(), dp);
                success = result > 0;
            }
            else
            {
                _logger.LogError("TradingEdit failed, some value is null");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "TradingEdit failed");
        }
        return success;
    }

    public async Task<bool> TradingDelete(string id)
    {
        bool success = false;
        try
        {
            if (!string.IsNullOrEmpty(id))
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("delete from CryptocurrencyTrading ");
                sql.AppendLine("where Id = @Id ");
                DynamicParameters dp = new DynamicParameters();
                dp.Add("@Id", id);

                int result = await _conn.ExecuteAsync(sql.ToString(), dp);
                success = result > 0;
            }
            else
            {
                _logger.LogError("TradingDelete failed, id is null");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "TradingDelete failed");
        }
        return success;
    }
}
