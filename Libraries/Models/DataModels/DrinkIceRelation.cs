
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Models.DataModels;

#nullable disable warnings

namespace Models.DataModels;
public class DrinkIceRelation : BaseDataModel
{

    [NotMapped]
    override public int Id { get; set; }

    [ForeignKey(nameof(Drink))]
    public int DrinkId { get; set; }

    [ForeignKey(nameof(Ice))]
    public int IceId { get; set; }


}
