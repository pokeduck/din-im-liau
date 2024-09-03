using System.ComponentModel.DataAnnotations;

namespace Models.Requests;

#nullable disable warnings

public class LoginRequest
{
    [Required]
    public string Account { get; set; }

    [Required]
    public string Password { get; set; }
}
