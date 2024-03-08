using CompanyPortal.ViewModels;

using FluentValidation;

namespace CompanyPortal.Components.Admin.Validators;

public class ProductFormValidator : AbstractValidator<ProductViewModel>
{
    public ProductFormValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Tên sản phẩm không được bỏ trống.");
        RuleFor(x => x.ShortDescription).NotEmpty().WithMessage("Mô tả ngắn của sản phẩm không được bỏ trống.");
        RuleFor(x => x.FullDescription).NotEmpty().WithMessage("Mô tả đầy đủ của sản phẩm không được bỏ trống.");
        RuleFor(x => x.CategoryId).GreaterThan(0).WithMessage("Danh mục cần được chọn.");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Bạn có chắc là sản phẩm này có giá là 0 đồng?.");
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