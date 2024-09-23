
using System.ComponentModel.DataAnnotations;
using Models.Attributes;

namespace Models.Requests;

#nullable disable warnings

public class UpdateProfileRequest
{
    public string? Name { get; set; }
    [EmailOptional]
    public string? Email { get; set; }


}
