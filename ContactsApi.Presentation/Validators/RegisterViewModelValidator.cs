using ContactsApi.Core.ViewModels;
using FluentValidation;

namespace ContactsApi.Presentation.Validators
{
    public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterViewModelValidator()
        {
            RuleFor(rvm => rvm.Username)
               .NotEmpty()
               .MaximumLength(20);

            RuleFor(rvm => rvm.Password)
               .NotEmpty()
               .MaximumLength(20);
        }
    }
}
