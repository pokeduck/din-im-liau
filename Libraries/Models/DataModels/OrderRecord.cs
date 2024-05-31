
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Models.DataModels;

#nullable disable warnings

namespace Models.DataModels;
public class OrderRecord : BaseDataModel, ICreateEntity, IUpdateEntity
{


    [ForeignKey(nameof(Order))]
    public int OrderId { get; set; }

    public int CustomerId { get; set; }
    public Account Customer { get; set; }

    [Required]
    [MaxLength(length: 20)]
    public string DrinkName { get; set; }
    public int Quantity { get; set; }

    public int TotalPrice { get; set; }

    /// <summary>
    /// 冰塊甜度加料 json object
    /// {Ice,[Topping]?,Suger}
    /// </summary> 
    /// <value></value>
    [MaxLength(length: 300)]
    public string Extra { get; set; }

    [Required]
    public long CreateTime { get; set; }

    [Required]
    public long UpdateTime { get; set; }

    public Order order { get; set; }

}
