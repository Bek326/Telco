using System.Data;
using System.Data.SqlClient;
using Dapper;
using Telco.Models;

namespace Telco.Data;

public class AbonentRepository
{
    private string connectionString;

    public AbonentRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public IEnumerable<Abonent> GetAllAbonents()
    {
        using (IDbConnection dbConnection = new SqlConnection(connectionString))
        {
            dbConnection.Open();
            return dbConnection.Query<Abonent>("SELECT * FROM Abonent");
        }
    }

    public void InsertAbonent(Abonent abonent)
    {
        using (IDbConnection dbConnection = new SqlConnection(connectionString))
        {
            dbConnection.Open();
            dbConnection.Execute("INSERT INTO Abonent (FullName) VALUES (@FullName)", abonent);
        }
    }

    public void UpdateAbonent(Abonent abonent)
    {
        using (IDbConnection dbConnection = new SqlConnection(connectionString))
        {
            dbConnection.Open();
            dbConnection.Execute("UPDATE Abonent SET FullName = @FullName WHERE AbonentId = @AbonentId", abonent);
        }
    }

    public void DeleteAbonent(int abonentId)
    {
        using (IDbConnection dbConnection = new SqlConnection(connectionString))
        {
            dbConnection.Open();
            dbConnection.Execute("DELETE FROM Abonent WHERE AbonentId = @AbonentId", new { AbonentId = abonentId });
        }
    }
}