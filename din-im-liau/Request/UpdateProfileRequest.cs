
using System.ComponentModel.DataAnnotations;

namespace din_im_liau.Request;

#nullable disable warnings

public class UpdateProfileRequest
{
    [Required]
    public string Name { get; set; }

}
