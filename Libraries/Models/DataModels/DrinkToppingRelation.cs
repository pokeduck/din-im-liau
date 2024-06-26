
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Models.DataModels;

#nullable disable warnings

namespace Models.DataModels;
public class DrinkToppingRelation : BaseDataModel
{

    [NotMapped]
    override public int Id { get; set; }

    [ForeignKey(nameof(Drink))]
    public int DrinkId { get; set; }

    [ForeignKey(nameof(Topping))]
    public int ToppingId { get; set; }


}
