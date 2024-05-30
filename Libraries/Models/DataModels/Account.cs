

using System.ComponentModel.DataAnnotations;
using Models.ViewModels;

#nullable disable warnings

namespace Models.DataModels;

public class Account : BaseDataModel, IUpdateEntity, ICreateEntity
{

    [Required]
    [MaxLength(length: 50)]
    public string Email { get; set; }
    [Required]
    [MaxLength(length: 50)]
    public string googleId { get; set; }

    [MaxLength(length: 50)]
    public string NickName { get; set; }
    [MaxLength(length: 200)]

    public string ThunbnailUrl { get; set; }

    [Required]
    [MaxLength(length: 50)]
    public string GoogleOpenId { get; set; }


    [Required]
    public string AccessToken { get; set; }

    [Required]
    public string Salt { get; set; }

    [Required]
    public long CreateTime { get; set; }

    [Required]
    public long UpdateTime { get; set; }

}
