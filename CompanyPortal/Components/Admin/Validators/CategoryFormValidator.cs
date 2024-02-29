using CompanyPortal.ViewModels;

using FluentValidation;

namespace CompanyPortal.Components.Admin.Validators;

public class CategoryFormValidator : AbstractValidator<CategoryViewModel>
{
    public CategoryFormValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
    }
}