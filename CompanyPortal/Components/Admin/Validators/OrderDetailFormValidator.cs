using CompanyPortal.Core.Enums;
using CompanyPortal.ViewModels;
using FluentValidation;

namespace CompanyPortal.Components.Admin.Validators;

public class OrderDetailFormValidator : AbstractValidator<OrderDetailViewModel>
{
    public OrderDetailFormValidator()
    {
        RuleFor(x => x.Quantity).GreaterThan(1);
    }

    /*private bool OptionalPropertiesAreValid(ProductViewModel obj, decimal? number)
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
    }*/
}