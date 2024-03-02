﻿using System.ComponentModel.DataAnnotations;
using infrastructure;
using infrastructure.DataModel;

namespace service;

public class DrawEntryService
{
    private readonly DrawEntryRepository _drawEntryRepository;

    public DrawEntryService(DrawEntryRepository drawEntryRepository)
    {
        _drawEntryRepository = drawEntryRepository;
    }



    public DrawEntry CreateEntry(string firstName, string lastName, string emailAddress,string serialNumber)
    {
        try
        {
            return _drawEntryRepository.CreateEntry(firstName, lastName, emailAddress,serialNumber);
        }
        catch (ValidationException e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine(e.InnerException?.Message);
            throw;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine(e.InnerException?.Message);
            throw new Exception("Could not create entry");
        }
    }
}