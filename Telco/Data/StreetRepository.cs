using System.Data;
using System.Data.SqlClient;
using Dapper;
using Telco.Models;

namespace Telco.Data;

public class StreetRepository
{
    private string connectionString;

    public StreetRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public IEnumerable<Street> GetAllStreets()
    {
        using (IDbConnection dbConnection = new SqlConnection(connectionString))
        {
            dbConnection.Open();
            return dbConnection.Query<Street>("SELECT * FROM Street");
        }
    }
    
    public void InsertStreet(Street street)
    {
        using (IDbConnection dbConnection = new SqlConnection(connectionString))
        {
            dbConnection.Open();
            dbConnection.Execute("INSERT INTO Street (FullName) VALUES (@FullName)", street);
        }
    }

    public void UpdateStreet(Street street)
    {
        using (IDbConnection dbConnection = new SqlConnection(connectionString))
        {
            dbConnection.Open();
            dbConnection.Execute("UPDATE Street SET FullName = @FullName WHERE StreetId = @StreetId", street);
        }
    }

    public void DeleteStreet(int streetId)
    {
        using (IDbConnection dbConnection = new SqlConnection(connectionString))
        {
            dbConnection.Open();
            dbConnection.Execute("DELETE FROM Street WHERE StreetId = @StreetId", new { StreetId = streetId });
        }
    }
}