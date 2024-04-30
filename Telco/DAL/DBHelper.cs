using System.Data.SqlClient;
using Dapper;

namespace Telco.DAL;

public class DbHelper
{
    private string _connectionString;

    public DbHelper(string connectionString)
    {
        this._connectionString = connectionString;
    }

    public List<T> Query<T>(string sql, object param = null)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            return connection.Query<T>(sql, param).AsList();
        }
    }

    public int Execute(string sql, object param = null)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            return connection.Execute(sql, param);
        }
    }
}