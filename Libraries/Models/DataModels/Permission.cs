

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.ViewModels;

#nullable disable warnings

namespace Models.DataModels;

public class Permission : BaseDataModel, IUpdateEntity, ICreateEntity
{

    [Required]
    [MaxLength(length: 20)]
    public string Name { get; set; }
    [Required]
    public bool IsAdmin { get; set; }

    [Required]
    public long CreateTime { get; set; }

    [Required]
    public long UpdateTime { get; set; }


    public ICollection<Account> Accounts { get; set; } = new HashSet<Account>();

}
