

using System.ComponentModel.DataAnnotations;
using Models.ViewModels;

#nullable disable warnings

namespace Models.DataModels;

public class Account : BaseDataModel, IUpdateEntity, ICreateEntity
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string googleId { get; set; }

    public string NickName { get; set; }

    public string ThunbnailUrl { get; set; }

    [Required]
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
