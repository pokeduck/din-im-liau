using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Enums;
using Microsoft.EntityFrameworkCore;
using Models.DataModels;

#nullable disable warnings

namespace Models.DataModels;
public class RefreshToken : BaseDataModel, ICreateEntity, IUpdateEntity
{

    [NotMapped]
    override public int Id { get; set; }


    [Required]
    [Key]
    public Guid Token { get; set; }


    [ForeignKey(nameof(DataModels.Account))]
    public int AccountId { get; set; }

    [Required]
    [DeleteBehavior(DeleteBehavior.Cascade)]
    public Account Account { get; init; }

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
