using ContactsApi.Core.ViewModels;
using FluentValidation;

namespace ContactsApi.Presentation.Validators
{
    public class SkillSaveViewModelValidator : AbstractValidator<SkillSaveViewModel>
    {
        public SkillSaveViewModelValidator()
        {
            RuleFor(svm => svm.Name)
               .NotEmpty()
               .MaximumLength(20);

            RuleFor(svm => svm.LevelId)
                .IsInEnum();
        }
    }
}
