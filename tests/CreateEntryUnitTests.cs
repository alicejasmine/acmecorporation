﻿using System.Net;
using System.Net.Http.Json;
using Dapper;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using NUnit.Framework;
using Newtonsoft.Json;

namespace tests;

public class CreateEntryUnitTests
{
    [TestCase("Andrea", "Clay", "andreac@gmail.com", "XHMXCLGXBFSUU959WLLW", true)]
    public async Task EntryCanSuccessfullyBeCreatedFromHttpRequest(string fullname, string lastname,
        string emailAddress, string serialNumber, bool isAgeConfirmed)
    {
        //ARRANGE
        Helper.TriggerRebuild();

        var testEntry = new CreateDrawEntryRequestDto
        {
            FirstName = fullname, LastName = lastname, EmailAddress = emailAddress, SerialNumber = serialNumber,
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
    
    
    [TestCase("Firstname that is long and exceeds the set character limit", "Clay", "andreac@gmail.com", "XHMXCLGXBFSUU959WLLW", true)] //firstname exceeds character limit
    [TestCase("Andrea", "Lastname that is long and exceeds the set character limit", "andreac@gmail.com", "XHMXCLGXBFSUU959WLLW", true)] //lastname exceeds character limit
    [TestCase("Andrea", "Clay", "andreac@gmail.com", "invalidSerialNumber", true)] //serial number invalid
    [TestCase("Andrea", "Clay", "notemailformat", "XHMXCLGXBFSUU959WLLW", true)] //Email format not valid
    [TestCase("Andrea", "Clay", "andreac@gmail.com", "XHMXCLGXBFSUU959WLLW", false)] //Age confirmation false
    public async Task ServerSideDataValidationShouldRejectEntry(string fullname, string lastname,
        string emailAddress, string serialNumber, bool isAgeConfirmed)
    {
        //ARRANGE
        Helper.TriggerRebuild();

        var testEntry = new CreateDrawEntryRequestDto
        {
            FirstName = fullname, LastName = lastname, EmailAddress = emailAddress, SerialNumber = serialNumber,
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
}