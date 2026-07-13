using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleCommandValidator : AbstractValidator<UpdateSaleCommand>
{
    public UpdateSaleCommandValidator()
    {
        RuleFor(x => x.Number)
            .GreaterThan(0)
            .WithMessage("Sale number must be greater than zero");
        
        RuleFor(x => x.Items)
            .NotEmpty()
            .WithMessage("Sale items must not be empty");

        RuleForEach(x => x.Items)
            .SetValidator(new UpdateSaleItemCommandValidator());
    }
}