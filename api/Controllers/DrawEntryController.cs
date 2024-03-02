using api.DataValidation;
using api.DTO;
using infrastructure.DataModel;
using Microsoft.AspNetCore.Mvc;
using service;

namespace api.Controllers;


[ApiController]

public class DrawEntryController : ControllerBase
{
    private readonly DrawEntryService _drawEntryService;

    public DrawEntryController(DrawEntryService drawEntryService)
    {
        _drawEntryService = drawEntryService;
    }

    // post draw entry

    [HttpPost]
    [ValidateModel]
    [Route("/api/entry")]
    public DrawEntry Post([FromBody] CreateDrawEntryRequestDto dto)
    {
        HttpContext.Response.StatusCode = StatusCodes.Status201Created;
        return _drawEntryService.CreateEntry(dto.FirstName, dto.LastName, dto.EmailAddress, dto.SerialNumber);
    }


}



