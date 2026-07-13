using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleItemCommandValidator : AbstractValidator<UpdateSaleItemCommand>
{
    public UpdateSaleItemCommandValidator()
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