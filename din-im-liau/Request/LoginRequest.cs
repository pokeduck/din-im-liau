using System.ComponentModel.DataAnnotations;

namespace din_im_liau.Request;

#nullable disable warnings

public class LoginRequest
{
    [Required]
    public string Account { get; set; }

    [Required]
    public string Password { get; set; }
}
