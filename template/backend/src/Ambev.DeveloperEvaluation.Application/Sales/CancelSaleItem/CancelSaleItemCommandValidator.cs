using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem;

public class CancelSaleItemCommandValidator : AbstractValidator<CancelSaleItemCommand>
{
    public CancelSaleItemCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Sale item ID is required");
    }
}