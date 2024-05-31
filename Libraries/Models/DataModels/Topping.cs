
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Models.DataModels;

#nullable disable warnings

namespace Models.DataModels;
public class Topping : BaseDataModel
{

    [Required]
    [MaxLength(length: 20)]
    public string Name { get; set; }
    [Required]
    public int Price { get; set; }

    public ICollection<DrinkToppingRelation> Drinks { get; set; } = new HashSet<DrinkToppingRelation>();


}
