using System.ComponentModel.DataAnnotations;
using Models.Attributes;

namespace Models.Requests;

#nullable disable warnings
public class AuthRegisterRequest
{


    [EmailRequired]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Require Username")]
    [MaxLength(length: 20)]
    public string? Username { get; set; }

    [PasswordRequired]
    public string? Password { get; set; }


}

public class EmailLayer2
{
    [Required]
    public EmailLayer3? Email { get; set; }

    [Required]
    public string? EmailHost { get; set; }
}
public class EmailLayer3
{

    [EmailRequired]
    public string? Email { get; set; }


    [EmailRequired]
    public string? Email2 { get; set; }
}
