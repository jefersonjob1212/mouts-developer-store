using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
{
    public CreateSaleRequestValidator()
    {
        RuleFor(x => x.ClientId)
            .NotEmpty()
            .WithMessage("Client is required");
        
        RuleFor(x => x.Items)
            .NotEmpty()
            .WithMessage("Items are required");
        
        RuleForEach(x => x.Items)
            .SetValidator(new CreateSaleItemRequestValidator());
    }
}