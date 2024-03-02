using Dapper;
using infrastructure.DataModel;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;


namespace infrastructure;

public class DrawEntryRepository
{
    private readonly string _connectionString;


    public DrawEntryRepository(IConfiguration config)
    {
        _connectionString = Utilities.GetConnectionString();
    }

    /*checks in db if serial number has been already used twice in the draw*/
    public bool IsSerialNumberLimitExceeded(string serialNumber)
    {
        var sql = @"SELECT COUNT(*) FROM dbo.DrawEntries WHERE serial_number=@serialNumber";
        using (var conn = new SqlConnection(_connectionString))
        {
            int count = conn.QuerySingle<int>(sql, new { serialNumber });
            return count >= 2;
        }
    }

 
    
    

    public DrawEntry CreateEntry(string firstName, string lastName, string emailAddress, string serialNumber)
    {
        var sql = $@"
                INSERT INTO dbo.DrawEntries (first_name, last_name, email_address, serial_number)
                OUTPUT INSERTED.*
                VALUES (@firstName, @lastName, @emailAddress, @serialNumber);
        
                 ;";

        using (var conn = new SqlConnection(_connectionString))

        {

            return conn.QueryFirst<DrawEntry>(sql, new
            {
                firstName,
                lastName,
                emailAddress,
                serialNumber
            });
        }
    }
}