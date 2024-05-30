
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Models.DataModels;

#nullable disable warnings

namespace Models.DataModels;
public class Ice : BaseDataModel
{

    [Required]
    [MaxLength(length: 20)]
    public string Name { get; set; }
    [Required]
    public int Ratio { get; set; }

}
