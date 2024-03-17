using CompanyPortal.ViewModels;

using FluentValidation;

namespace CompanyPortal.Components.Admin.Validators;

public class CategoryFormValidator : AbstractValidator<CategoryViewModel>
{
    public CategoryFormValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Tên danh mục không được bỏ trống.");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Mô tả ngắn của danh mục không được bỏ trống.");
        RuleFor(x => x.ExternalId).NotEmpty().WithMessage("Mã danh mục không được bỏ trống.");
        RuleFor(x => x.Image.Name).NotEmpty().WithMessage("Ảnh mô tả không được bỏ trống.");
    }
}