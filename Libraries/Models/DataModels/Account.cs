

using System.ComponentModel.DataAnnotations;

#nullable disable warnings

namespace Models.DataModels;

class Account
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string googleId { get; set; }

    [Required]
    public string NickName { get; set; }

    [Required]
    public string ThunbnailUrl { get; set; }


}
