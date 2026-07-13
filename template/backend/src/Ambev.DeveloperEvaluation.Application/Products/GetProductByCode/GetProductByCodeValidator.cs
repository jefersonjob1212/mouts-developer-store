using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProductByCode;

/// <summary>
/// Validator for GetProductByCodeCommand
/// </summary>
public class GetProductByCodeValidator : AbstractValidator<GetProductByCodeCommand>
{
    /// <summary>
    /// Initializes validation rules for GetProductByCodeCommand
    /// </summary>
    public GetProductByCodeValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty()
            .WithMessage("Product code is required");
    }
}