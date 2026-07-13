using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleItemCommandValidator : AbstractValidator<CreateSaleItemCommand>
{
    public CreateSaleItemCommandValidator()
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