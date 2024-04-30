using System.Data;
using System.Data.SqlClient;
using Dapper;
using Telco.Models;

namespace Telco.Data;

public class PhoneNumberRepository
{
    private string connectionString;

    public PhoneNumberRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public IEnumerable<PhoneNumber> GetAllPhoneNumbers()
    {
        using (IDbConnection dbConnection = new SqlConnection(connectionString))
        {
            dbConnection.Open();
            return dbConnection.Query<PhoneNumber>("SELECT * FROM PhoneNumber");
        }
    }
    
    public IEnumerable<Abonent> GetAbonentsByPhoneNumber(string phoneNumber)
    {
        using (IDbConnection dbConnection = new SqlConnection(connectionString))
        {
            dbConnection.Open();
            // Здесь предполагается, что в базе данных есть хранимая процедура или запрос, который возвращает абонентов по номеру телефона
            // Вам нужно заменить "YourStoredProcedureName" на имя вашей хранимой процедуры или написать SQL-запрос соответствующим образом
            //string sqls = "EXEC YourStoredProcedureName @PhoneNumber";
            string sql = "SELECT * FROM Abonent WHERE PhoneNumber = @PhoneNumber";
            return dbConnection.Query<Abonent>(sql, new { PhoneNumber = phoneNumber });
        }
    }
    
    public void InsertPhoneNumber(PhoneNumber phoneNumber)
    {
        using (IDbConnection dbConnection = new SqlConnection(connectionString))
        {
            dbConnection.Open();
            dbConnection.Execute("INSERT INTO PhoneNumber (FullName) VALUES (@FullName)", phoneNumber);
        }
    }

    public void UpdatePhoneNumber(PhoneNumber phoneNumber)
    {
        using (IDbConnection dbConnection = new SqlConnection(connectionString))
        {
            dbConnection.Open();
            dbConnection.Execute("UPDATE PhoneNumber SET FullName = @FullName WHERE PhoneNumbertId = @PhoneNumbertId", phoneNumber);
        }
    }

    public void DeletePhoneNumber(int phoneNumberId)
    {
        using (IDbConnection dbConnection = new SqlConnection(connectionString))
        {
            dbConnection.Open();
            dbConnection.Execute("DELETE FROM PhoneNumber WHERE PhoneNumbertId = @PhoneNumberId", new { PhoneNumberId = phoneNumberId });
        }
    }
}