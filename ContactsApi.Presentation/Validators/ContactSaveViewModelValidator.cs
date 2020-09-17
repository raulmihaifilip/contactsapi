using ContactsApi.Core.ViewModels;
using FluentValidation;

namespace ContactsApi.Presentation.Validators
{
    public class ContactSaveViewModelValidator : AbstractValidator<ContactSaveViewModel>
    {
        public ContactSaveViewModelValidator()
        {
            RuleFor(cvm => cvm.Firstname)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(cvm => cvm.Lastname)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(cvm => cvm.Address)
                .NotEmpty()
                .MaximumLength(40);

            RuleFor(cvm => cvm.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(20);

            RuleFor(cvm => cvm.MobilePhoneNumber)
                .NotEmpty()
                .Length(10, 15);
        }
    }
}
