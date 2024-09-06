
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Models.DTOs;

#nullable disable warnings

public class AccountDTO
{
    [Required]
    [DataMember(Order = 0)]
    public int? Uid { get; set; }

    [Required]
    public string? Nickname { get; set; }

    [Required]
    public bool? IsEmailVerified { get; set; }



}
