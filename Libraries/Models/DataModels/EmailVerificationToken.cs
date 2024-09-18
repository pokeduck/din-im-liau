
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Enums;
using Microsoft.EntityFrameworkCore;
using Models.DataModels;

#nullable disable warnings

namespace Models.DataModels;
public class EmailVerificationToken : BaseDataModel, ICreateEntity, IUpdateEntity
{

    [NotMapped]
    override public int Id { get; set; }


    [Required]
    [Key]
    public Guid Token { get; set; }


    [ForeignKey(nameof(Account))]
    public int AccountId { get; set; }

    [Required]
    public TokenStatus Status { get; set; }


    [Required]
    [DefaultValue(0)]
    public long ExpireTime { get; set; }

    [Required]
    public long CreateTime { get; set; }

    [Required]
    public long UpdateTime { get; set; }


}
