using din_im_liau.Request;
using FluentValidation;

public class AuthRegisterValidator : AbstractValidator<AuthRegisterRequest>
{
    public AuthRegisterValidator()
    {
        // RuleFor(x => x.Account).NotEmpty().WithMessage("Account不可為空");
        // RuleFor(x => x.Password).NotEmpty().WithMessage("Pwd請輸入密碼");
        // RuleFor(x => x.Email).NotEmpty().WithMessage("Email不可為空");
    }
}
