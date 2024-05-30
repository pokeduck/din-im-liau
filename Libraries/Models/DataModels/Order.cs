
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Models.DataModels;

#nullable disable warnings

namespace Models.DataModels;
public class Order : BaseDataModel, ICreateEntity, IUpdateEntity
{

    [Required]
    public long EndTime { get; set; }
    [ForeignKey(nameof(Store))]
    public int StoreId { get; set; }
    [ForeignKey(nameof(Account))]
    public int AdminId { get; set; }

    public int TotalPrice { get; set; }



    [Required]
    public long CreateTime { get; set; }

    [Required]
    public long UpdateTime { get; set; }

    public ICollection<OrderRecord> orderRecords { get; set; } = new HashSet<OrderRecord>();

}
