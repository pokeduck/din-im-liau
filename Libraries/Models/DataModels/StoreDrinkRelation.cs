
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Models.DataModels;

#nullable disable warnings

namespace Models.DataModels;
public class StoreDrinkRelation : BaseDataModel
{


    [ForeignKey(nameof(Store))]
    public int StoreId { get; set; }
    [ForeignKey(nameof(Drink))]
    public int DrinkId { get; set; }

}
