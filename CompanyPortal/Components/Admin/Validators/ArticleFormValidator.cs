using CompanyPortal.ViewModels;

using FluentValidation;

namespace CompanyPortal.Components.Admin.Validators;

public class ArticleFormValidator : AbstractValidator<ArticleViewModel>
{
    public ArticleFormValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Type).IsInEnum();
        RuleFor(x => x.Content).NotEmpty();
    }
}