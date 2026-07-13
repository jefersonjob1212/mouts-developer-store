using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

public class UpdateSaleRequestValidator : AbstractValidator<UpdateSaleRequest>
{
    public UpdateSaleRequestValidator()
    {
        RuleFor(x => x.ClientId)
            .NotEmpty()
            .WithMessage("Client is required");
        
        RuleFor(x => x.Items)
            .NotEmpty()
            .WithMessage("Items are required");
        
        RuleForEach(x => x.Items)
            .SetValidator(new UpdateSaleItemRequestValidator());
    }
}