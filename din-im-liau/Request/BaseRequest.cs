using System.ComponentModel.DataAnnotations;


#nullable disable warnings
namespace din_im_liau.Request
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
