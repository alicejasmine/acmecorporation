using System.ComponentModel.DataAnnotations;
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


    public DrawEntry CreateEntry()
    {
        try
        {
            //if (_drawEntryRepository.IsFullNameTakenInCreate(fullname))
              //  throw new ValidationException("player name is taken");
            return _drawEntryRepository.CreateEntry();
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
