
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Models.DataModels;

#nullable disable warnings

namespace Models.DataModels;
public class ServingSize : BaseDataModel
{

    [Required]
    [MaxLength(length: 5)]
    public string Name { get; set; }
    [Required]
    public int PriceGap { get; set; }

}
