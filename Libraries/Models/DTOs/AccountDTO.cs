
using System.ComponentModel.DataAnnotations;

namespace Models.DTOs;

#nullable disable warnings

public class AccountDTO
{
    [Required]
    public int? Uid { get; set; }

    [Required]
    public string? NickName { get; set; }

    [Required]
    public bool? IsEmailValid { get; set; }


}
