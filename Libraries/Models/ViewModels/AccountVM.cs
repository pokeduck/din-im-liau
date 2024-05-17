
using System.ComponentModel.DataAnnotations;

namespace Models.ViewModels;

public class AccountVM : BaseViewModel
{
    [Required(ErrorMessage = "帳號為必填")]
    public string? Email { get; set; }

}
