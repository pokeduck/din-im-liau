

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using Models.ViewModels;
using System.ComponentModel;

#nullable disable warnings

namespace Models.DataModels;

public class Account : BaseDataModel, IUpdateEntity, ICreateEntity
{

    [Required]
    [MaxLength(length: 50)]
    public string Email { get; set; }

    [DataMember(Order = 1)]
    [MaxLength(length: 50)]
    public string NickName { get; set; }
    [MaxLength(length: 200)]


    public string? ThumbnailUrl { get; set; }

    [Required]
    [MaxLength(length: 50)]
    [DefaultValue("")]
    public string GoogleOpenId { get; set; }

    public int PermissionId { get; set; }

    [DefaultValue(0)]
    public int EmailValidStatus { get; set; }

    public string? AccessToken { get; set; }

    public string? Salt { get; set; }

    [Required]
    public long CreateTime { get; set; }

    [Required]
    public long UpdateTime { get; set; }



    public Permission Permission { get; set; }

    public ICollection<Order> Orders { get; set; } = new List<Order>();

    public ICollection<OrderRecord> OrderRecords { get; set; } = new HashSet<OrderRecord>();
}
