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
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("insert into CoinType (Name) ");
            sql.AppendLine("values (@Name) ");
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@Name", coin.Name);

            int result = await _conn.ExecuteAsync(sql.ToString(), dp);
            success = result > 0;
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
        catch (Exception ex)
        {
            _logger.LogError(ex, "EditCoin failed");
        }
        return success;
    }

    public async Task<bool> DeleteCoin(int id)
    {
        bool success = false;
        try
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("delete from CoinType ");
            sql.AppendLine("where Id = @Id ");
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@Id", id);

            int result = await _conn.ExecuteAsync(sql.ToString(), dp);
            success = result > 0;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "DeleteCoin failed");
        }
        return success;
    }
}
