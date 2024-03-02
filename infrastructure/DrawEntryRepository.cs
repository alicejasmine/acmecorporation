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

    public DrawEntry CreateEntry(string firstName, string lastName, string emailAddress, string serialNumber)
    {
        var sql = $@"
                INSERT INTO dbo.DrawEntries (first_name, last_name, email_address, serial_number)
                OUTPUT INSERTED.*
                VALUES (@firstName, @lastName, @emailAddress, @serialNumber);
        
                 ;";

        using (var conn = new SqlConnection(_connectionString))

        {
            Console.WriteLine("here: " + _connectionString);

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