using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProductsByName;

/// <summary>
/// Validator for GetProductByIdCommand
/// </summary>
public class GetProductsByNameValidator : AbstractValidator<GetProductsByNameCommand>
{
    /// <summary>
    /// Initializes validation rules for GetProductByIdCommand
    /// </summary>
    public GetProductsByNameValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Product name is required");
    }
}