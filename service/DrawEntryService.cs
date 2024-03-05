using System.ComponentModel.DataAnnotations;
using infrastructure;
using infrastructure.DataModel;
using Microsoft.Extensions.Logging;

namespace service;

public class DrawEntryService
{
    private readonly ILogger<DrawEntryService> _logger;
    private readonly DrawEntryRepository _drawEntryRepository;

    public DrawEntryService(DrawEntryRepository drawEntryRepository, ILogger<DrawEntryService> logger)
    {
        _drawEntryRepository = drawEntryRepository;
        _logger = logger;
    }


    public DrawEntry CreateEntry(string firstName, string lastName, string emailAddress, string serialNumber, bool isOver18Confirmed)
    {
        try
        {
            ValidateAge(isOver18Confirmed);
            ValidateSerialNumber(serialNumber);
            ValidateSerialNumberLimit(serialNumber);
            return _drawEntryRepository.CreateEntry(firstName, lastName, emailAddress, serialNumber);
        }
        catch (ValidationException e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine(e.InnerException?.Message);
            _logger.LogError(e, "Validation failed while creating the entry. Validation errors: {ValidationErrors}", e.Message);
            throw;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine(e.InnerException?.Message);
            _logger.LogError(e, "An error occurred while creating the entry.");
            throw new Exception("Could not create entry");
        }
    }


    private void ValidateSerialNumberLimit(string serialNumber)
    {
        if (_drawEntryRepository.IsSerialNumberLimitExceeded(serialNumber))
        {
            throw new ValidationException($"Maximum number of entries for serial number {serialNumber} reached");
        }
    }

    private void ValidateSerialNumber(string serialNumber)
    {
        if (!_drawEntryRepository.IsSerialNumberValid(serialNumber))
        {
            throw new ValidationException($"The serial number: {serialNumber} is not valid");
        }
    }

    private void ValidateAge(bool isOver18Confirmed)
    {
        if (!isOver18Confirmed)
        {
            throw new ValidationException("User must confirm to be 18 years or older to enter the draw.");
        }
    }
    public IEnumerable<DrawEntry> GetEntries(int page, int resultsPerPage)
    {
        return _drawEntryRepository.GetEntries(page, resultsPerPage);
    }
    
    
}