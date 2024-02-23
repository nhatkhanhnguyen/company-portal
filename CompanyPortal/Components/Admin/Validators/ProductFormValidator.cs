using CompanyPortal.ViewModels;

using FluentValidation;

namespace CompanyPortal.Components.Admin.Validators;

public class ProductFormValidator : AbstractValidator<ProductViewModel>
{
    public ProductFormValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.Price).NotEmpty().GreaterThan(0);
        RuleFor(x => x.DiscountedPrice).Must(OptionalPropertiesAreValid).WithMessage("Only one of DiscountedPrice and PercentDiscount could be set");
    }

    private bool OptionalPropertiesAreValid(ProductViewModel obj, decimal? number)
    {
        if (obj.DiscountedPrice is null && obj.PercentDiscount is null)
        {
            return true;
        }

        return new[]
            {
                obj.DiscountedPrice is not null,
                obj.PercentDiscount is not null
            }
            .Count(x => x == true) == 1;
        // yes, the "== true" is not needed, I think it looks better
    }
}