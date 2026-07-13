using Ambev.DeveloperEvaluation.WebApi.Features.Clients.GetClient;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;

public class GetProductByCodeRequestValidator : AbstractValidator<GetProductByCodeRequest>
{
    public GetProductByCodeRequestValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty()
            .WithMessage("Code is required");
    }
}