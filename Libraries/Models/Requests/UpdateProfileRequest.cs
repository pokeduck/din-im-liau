
using System.ComponentModel.DataAnnotations;

namespace Models.Requests;

#nullable disable warnings

public class UpdateProfileRequest
{
    [Required]
    public string Name { get; set; }

}
