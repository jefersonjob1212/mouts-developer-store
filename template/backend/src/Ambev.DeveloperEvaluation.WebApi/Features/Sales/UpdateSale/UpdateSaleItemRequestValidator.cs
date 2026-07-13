using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

public class UpdateSaleItemRequestValidator : AbstractValidator<UpdateSaleItemRequest>
{
    public UpdateSaleItemRequestValidator()
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