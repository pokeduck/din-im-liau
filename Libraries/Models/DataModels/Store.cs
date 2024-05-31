
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Models.DataModels;

#nullable disable warnings

namespace Models.DataModels;
public class Store : BaseDataModel
{

    [Required]
    [MaxLength(length: 20)]
    public string Name { get; set; }
    [Required]
    [MaxLength(length: 50)]
    public string Address { get; set; }

    [MaxLength(length: 100)]
    public string MenuLink { get; set; }
    [MaxLength(length: 20)]
    public string Telephone { get; set; }
    [MaxLength(length: 30)]
    public string Memo { get; set; }

    public ICollection<Order> Orders { get; set; } = new List<Order>();
}



