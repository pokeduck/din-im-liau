
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Models.DataModels;

#nullable disable warnings

namespace Models.DataModels;
public class Drink : BaseDataModel, ICreateEntity, IUpdateEntity
{

    [Required]
    [MaxLength(length: 20)]
    public string Name { get; set; }
    [Required]
    public int Price { get; set; }

    [Required]
    public int ToppingLowerLimit { get; set; }
    [Required]
    public int ToppingUpperLimit { get; set; }
    [MaxLength(length: 200)]
    public string MenuLink { get; set; }

    [MaxLength(length: 30)]
    public string Memo { get; set; }


    [Required]
    public long CreateTime { get; set; }

    [Required]
    public long UpdateTime { get; set; }
}
