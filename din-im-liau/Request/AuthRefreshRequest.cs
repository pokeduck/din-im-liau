using System.ComponentModel.DataAnnotations;

namespace din_im_liau.Request;

#nullable disable warnings

public class AuthRefreshRequest
{
    [Required]
    public string RefreshToken { get; set; }

}
