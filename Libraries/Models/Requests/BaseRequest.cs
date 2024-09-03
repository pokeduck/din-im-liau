using System.ComponentModel.DataAnnotations;


#nullable disable warnings
namespace Models.Requests
{
    public class BaseConditionRequest
    {
        [Required]
        public int? Offset { get; set; }

        [Required]

        public int? PageSize { get; set; }
    }

    public class BaseIdRequest
    {
        [Required]
        public int? Id { get; set; }
    }

}
