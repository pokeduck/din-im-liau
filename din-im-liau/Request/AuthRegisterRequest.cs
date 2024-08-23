using System.ComponentModel.DataAnnotations;
using din_im_liau.Attributes;

namespace din_im_liau.Request;

#nullable disable warnings
public class AuthRegisterRequest
{
    [Required(ErrorMessage = "Require Username")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Require Username")]
    public string? Password { get; set; }

    [Required]
    public EmailLayer2? Email { get; set; }

    [Required(ErrorMessage = "Require Account")]
    public string? Account { get; set; }
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
