using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

public class GetSaleByIdRequestValidator : AbstractValidator<GetSaleByIdRequest>
{
    public GetSaleByIdRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Sale id must not be empty");
    }
}