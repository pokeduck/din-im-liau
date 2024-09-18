using System.ComponentModel.DataAnnotations;

namespace Models.Requests.Auth;

public class ModifyPasswordRequest
{

    [Required]
    string oldPassword { get; set; }

    [Required]
    string newPassword { get; set; }
}


