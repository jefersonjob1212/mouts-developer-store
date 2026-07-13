using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleItemRequestValidator : AbstractValidator<CreateSaleItemRequest>
{
    public CreateSaleItemRequestValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage("Product is required");
        
        RuleFor(x => x.Quantity)
            .Must(ValidateQuantity)
            .WithMessage("Quantity must between 0 and 20");
    }

    private bool ValidateQuantity(int quantity)
        => quantity is > 0 and <= 20;
}