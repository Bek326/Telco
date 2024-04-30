using System.Data;
using System.Data.SqlClient;
using Dapper;
using Telco.Models;

namespace Telco.Data;

public class AddressRepository
{
    private string connectionString;

    public AddressRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public IEnumerable<Address> GetAllAddresses()
    {
        using (IDbConnection dbConnection = new SqlConnection(connectionString))
        {
            dbConnection.Open();
            return dbConnection.Query<Address>("SELECT * FROM Address");
        }
    }

    public void InsertAddress(Address address)
    {
        using (IDbConnection dbConnection = new SqlConnection(connectionString))
        {
            dbConnection.Open();
            dbConnection.Execute("INSERT INTO Address (AbonentId, StreetId, HouseNumber) VALUES (@AbonentId, @StreetId, @HouseNumber)", address);
        }
    }

    public void UpdateAddress(Address address)
    {
        using (IDbConnection dbConnection = new SqlConnection(connectionString))
        {
            dbConnection.Open();
            dbConnection.Execute("UPDATE Address SET FullName = @FullName WHERE AddresstId = @AbonentId", address);
        }
    }

    public void DeleteAddress(int addressId)
    {
        using (IDbConnection dbConnection = new SqlConnection(connectionString))
        {
            dbConnection.Open();
            dbConnection.Execute("DELETE FROM Address WHERE AddressId = @AddressId", new { AddressId = addressId });
        }
    }
}