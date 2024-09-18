


using System.ComponentModel.DataAnnotations;

namespace Models.Requests.Auth;

public class AuthForgotPasswordRequest
{
    [Required]
    string token { get; set; }
}
