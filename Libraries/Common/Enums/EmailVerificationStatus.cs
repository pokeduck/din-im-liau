using System.ComponentModel.DataAnnotations;

namespace Common.Enums;

public enum EmailVerificationStatus
{
    [Display(Name = "未驗證")]
    invalid = 0,
    [Display(Name = "已驗證")]
    valid = 1
}
