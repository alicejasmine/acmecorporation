using System.ComponentModel.DataAnnotations;

namespace api.DTO;

public class CreateDrawEntryRequestDto
{
    [Required] [MaxLength(50)] public string FirstName { get; set; }

    [Required] [MaxLength(50)] public string LastName { get; set; }

    [Required] [EmailAddress] public string EmailAddress { get; set; }

    [Required] [MaxLength(20)] public string SerialNumber { get; set; }
}