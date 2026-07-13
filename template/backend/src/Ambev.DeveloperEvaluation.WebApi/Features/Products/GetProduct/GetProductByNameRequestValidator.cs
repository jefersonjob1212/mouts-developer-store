using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;

public class GetProductByNameRequestValidator : AbstractValidator<GetProductByNameRequest>
{
    public GetProductByNameRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required");
    }
}