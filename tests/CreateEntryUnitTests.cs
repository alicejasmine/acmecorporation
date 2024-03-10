using System.Net;
using System.Net.Http.Json;
using Dapper;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using NUnit.Framework;
using Newtonsoft.Json;

namespace tests;

public class CreateEntryUnitTests
{
    [TestCase("Andrea", "Clay", "andreac@gmail.com", "968XZBC5GDXP19P7KA7Z", true)]
    public async Task EntryCanSuccessfullyBeCreatedFromHttpRequest(string firstname, string lastname,
        string emailAddress, string serialNumber, bool isAgeConfirmed)
    {
        //ARRANGE
        Helper.TriggerRebuild();

        var testEntry = new CreateDrawEntryRequestDto
        {
            FirstName = firstname, LastName = lastname, EmailAddress = emailAddress, SerialNumber = serialNumber,
            IsOver18Confirmed = isAgeConfirmed
        };

        //ACT
        var httpResponse = await new HttpClient().PostAsJsonAsync(Helper.ApiBaseUrl + "/entry", testEntry);
        DrawEntry entryFromResponseBody =
            JsonConvert.DeserializeObject<DrawEntry>(await httpResponse.Content.ReadAsStringAsync());

        //ASSERT
        await using (var conn = new SqlConnection(Helper.GetConnectionString()))

        {
            var resultEntry = conn.QueryFirst<DrawEntry>(
                "SELECT * FROM dbo.DrawEntries;");
            resultEntry.Should()
                .BeEquivalentTo(entryFromResponseBody);
        }
    }


    [TestCase("Firstname that is long and exceeds the set character limit", "Clay", "andreac@gmail.com",
        "968XZBC5GDXP19P7KA7Z", true)] //firstname exceeds character limit
    [TestCase("Andrea", "Lastname that is long and exceeds the set character limit", "andreac@gmail.com",
        "968XZBC5GDXP19P7KA7Z", true)] //lastname exceeds character limit
    [TestCase("Andrea", "Clay", "andreac@gmail.com", "invalidSerialNumber", true)] //serial number invalid
    [TestCase("Andrea", "Clay", "notemailformat", "968XZBC5GDXP19P7KA7Z", true)] //Email format not valid
    [TestCase("Andrea", "Clay", "andreac@gmail.com", "968XZBC5GDXP19P7KA7Z", false)] //Age confirmation false
    public async Task ServerSideDataValidationShouldRejectEntry(string firstname, string lastname,
        string emailAddress, string serialNumber, bool isAgeConfirmed)
    {
        //ARRANGE
        Helper.TriggerRebuild();

        var testEntry = new CreateDrawEntryRequestDto
        {
            FirstName = firstname, LastName = lastname, EmailAddress = emailAddress, SerialNumber = serialNumber,
            IsOver18Confirmed = isAgeConfirmed
        };

        //ACT
        var httpResponse = await new HttpClient().PostAsJsonAsync(Helper.ApiBaseUrl + "/entry", testEntry);
        DrawEntry entryFromResponseBody =
            JsonConvert.DeserializeObject<DrawEntry>(await httpResponse.Content.ReadAsStringAsync());


        // ASSERT
        httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using (var conn = new SqlConnection(Helper.GetConnectionString()))

        {
            conn.ExecuteScalar<int>("SELECT COUNT(*) FROM  dbo.DrawEntries;").Should().Be(0);
        }
    }


    [TestCase("Andrea", "Clay", "andreac@gmail.com", "968XZBC5GDXP19P7KA7Z", true)]
    public async Task ServerSideDataValidationShouldRejectEntryLimit(string firstname, string lastname,
        string emailAddress, string serialNumber, bool isAgeConfirmed)
    {
        //ARRANGE
        Helper.TriggerRebuild();

        var testEntry = new CreateDrawEntryRequestDto
        {
            FirstName = firstname, LastName = lastname, EmailAddress = emailAddress, SerialNumber = serialNumber,
            IsOver18Confirmed = isAgeConfirmed
        };

        await using (var conn = new SqlConnection(Helper.GetConnectionString()))

        {
            var sql =
                $"INSERT INTO dbo.DrawEntries(first_name, last_name, email_address, serial_number) VALUES (@{nameof(firstname)}, @{nameof(lastname)}, @{nameof(emailAddress)}, @{nameof(serialNumber)})";

            //add two entries with same serial number
            conn.Execute(sql, testEntry);
            conn.Execute(sql, testEntry);
        }


        //ACT
        var httpResponse = await new HttpClient().PostAsJsonAsync(Helper.ApiBaseUrl + "/entry", testEntry);
        DrawEntry entryFromResponseBody =
            JsonConvert.DeserializeObject<DrawEntry>(await httpResponse.Content.ReadAsStringAsync());


        // ASSERT
        httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using (var conn = new SqlConnection(Helper.GetConnectionString()))

        {
            conn.ExecuteScalar<int>("SELECT COUNT(*) FROM  dbo.DrawEntries;").Should()
                .Be(2); //3rd entry with same serial number is not inserted
        }
        Helper.TriggerRebuild();
    }
}