using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;

namespace CompanyPortal.Components.Admin.Validators;

public class FluentValidationValidator : ComponentBase
{
    [Inject] private IServiceProvider ServiceProvider { get; set; } = default!;
    [CascadingParameter] private EditContext EditContext { get; set; } = default!;
    [Parameter] public required Type ValidatorType { get; set; }

    private IValidator? _validator;
    private ValidationMessageStore? _validationMessageStore;

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        // Keep a reference to the original values so we can check if they have changed
        var previousEditContext = EditContext;
        var previousValidatorType = ValidatorType;

        await base.SetParametersAsync(parameters);

        if (EditContext == null)
            throw new NullReferenceException($"{nameof(FluentValidationValidator)} must be placed within an {nameof(EditForm)}");

        if (ValidatorType == null)
            throw new NullReferenceException($"{nameof(ValidatorType)} must be specified.");

        if (!typeof(IValidator).IsAssignableFrom(ValidatorType))
            throw new ArgumentException($"{ValidatorType.Name} must implement {typeof(IValidator).FullName}");

        if (ValidatorType != previousValidatorType)
            ValidatorTypeChanged();

        // If the EditForm.Model changes then we get a new EditContext
        // and need to hook it up
        if (EditContext != previousEditContext)
        {
            EditContextChanged();
        }
    }

    private async void ValidationRequested(object? sender, ValidationRequestedEventArgs args)
    {
        _validationMessageStore?.Clear();
        var validationContext =
            new ValidationContext<object>(EditContext.Model);
        var result = await _validator?.ValidateAsync(validationContext)!;
        AddValidationResult(EditContext.Model, result);
    }

    private async void FieldChanged(object? sender, FieldChangedEventArgs args)
    {
        var fieldIdentifier = args.FieldIdentifier;
        _validationMessageStore?.Clear(fieldIdentifier);

        var propertiesToValidate = new[] { fieldIdentifier.FieldName };
        var fluentValidationContext =
            new ValidationContext<object>(
                instanceToValidate: fieldIdentifier.Model,
                propertyChain: new FluentValidation.Internal.PropertyChain(),
                validatorSelector: new FluentValidation.Internal.MemberNameValidatorSelector(propertiesToValidate)
            );

        var result = await _validator?.ValidateAsync(fluentValidationContext)!;

        AddValidationResult(fieldIdentifier.Model, result);
    }

    private void ValidatorTypeChanged()
    {
        _validator = (IValidator)ServiceProvider.GetService(ValidatorType)!;
    }

    private void EditContextChanged()
    {
        _validationMessageStore = new ValidationMessageStore(EditContext);
        HookUpEditContextEvents();
    }

    private void HookUpEditContextEvents()
    {
        EditContext.OnValidationRequested += ValidationRequested;
        EditContext.OnFieldChanged += FieldChanged;
    }

    private void AddValidationResult(object model, ValidationResult validationResult)
    {
        foreach (var error in validationResult.Errors)
        {
            var fieldIdentifier = new FieldIdentifier(model, error.PropertyName);
            _validationMessageStore?.Add(fieldIdentifier, error.ErrorMessage);
        }
        EditContext.NotifyValidationStateChanged();
    }
}