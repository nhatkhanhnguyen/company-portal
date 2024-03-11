using CompanyPortal.Core.Enums;
using CompanyPortal.ViewModels;
using FluentValidation;

namespace CompanyPortal.Components.Admin.Validators;

public class OrderFormValidator : AbstractValidator<OrderViewModel>
{
    public OrderFormValidator()
    {
        RuleFor(x => x.Fullname).NotEmpty().WithMessage("Tên người đặt hàng không được bỏ trống.");
        RuleFor(x => x.Address).NotEmpty().WithMessage("Địa chỉ không được bỏ trống.");
        RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Số điện thoại người đặt hàng không được bỏ trống.");
        RuleFor(x => x.Status).IsInEnum().WithMessage("Trạng thái đơn hàng không được bỏ trống.");
        RuleFor(x => x.ExternalId).NotEmpty().WithMessage("Mã đơn hàng không được bỏ trống.");
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