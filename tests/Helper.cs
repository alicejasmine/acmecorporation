using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace tests;

public class Helper
{
    public static readonly string ApiBaseUrl = "http://localhost:5000/api";
    private static readonly string ConnectionString = Environment.GetEnvironmentVariable("sqlconn")!;


    public static readonly string RebuildScript = $@"

USE [AcmeDB]; -- Replace with your actual database name

IF OBJECT_ID('dbo.DrawEntries', 'U') IS NOT NULL
    DROP TABLE dbo.DrawEntries;


IF NOT EXISTS (SELECT 1 FROM sys.schemas WHERE name = 'dbo')
    EXEC('CREATE SCHEMA dbo;');


create table dbo.DrawEntries(
  entry_ID INT PRIMARY KEY IDENTITY(1,1),
    first_name NVARCHAR(50) NOT NULL,
    last_name NVARCHAR(50) NOT NULL,
    email_address NVARCHAR(255) NOT NULL,
    serial_number NVARCHAR(20) NOT NULL
);";


    static Helper()
    {
        if (string.IsNullOrEmpty(ConnectionString))
        {
            throw new Exception($@"The conn string sqlconn is empty.");
        }
    }

    public static void TriggerRebuild()
    {
        using (var conn = new SqlConnection(ConnectionString))

        {
            try
            {
                conn.Execute(RebuildScript);
            }
            catch (Exception e)
            {
                throw new Exception($"There was an error rebuilding the database.", e);
            }
        }
    }

    public static string GetConnectionString()
    {
        return ConnectionString;
    }
}