using System.ComponentModel.DataAnnotations;

namespace Models.Requests;

#nullable disable warnings

public class AuthRefreshRequest
{
    [Required]
    public string RefreshToken { get; set; }

}
