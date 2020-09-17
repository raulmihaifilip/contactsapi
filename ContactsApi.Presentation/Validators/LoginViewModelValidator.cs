using ContactsApi.Core.ViewModels;
using FluentValidation;

namespace ContactsApi.Presentation.Validators
{
    public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator()
        {
            RuleFor(lvm => lvm.Username)
               .NotEmpty()
               .MaximumLength(20);

            RuleFor(lvm => lvm.Password)
               .NotEmpty()
               .MaximumLength(20);
        }
    }
}
