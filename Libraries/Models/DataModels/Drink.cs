
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

    // [ForeignKey(nameof(Ice))]
    // public ICollection<Ice> Ices { get; set; } = new HashSet<Ice>();

    // [ForeignKey(nameof(Sugger))]
    // public ICollection<Sugger> Suggers { get; set; } = new HashSet<Sugger>();

    public ICollection<DrinkToppingRelation> ToppingRelations { get; set; } = new HashSet<DrinkToppingRelation>();
    public ICollection<DrinkIceRelation> IceRelations { get; set; } = new HashSet<DrinkIceRelation>();
    public ICollection<DrinkSuggerRelation> DrinkRelations { get; set; } = new HashSet<DrinkSuggerRelation>();
}
