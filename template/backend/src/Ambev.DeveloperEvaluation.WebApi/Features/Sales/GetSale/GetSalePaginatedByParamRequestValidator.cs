using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

public class GetSalePaginatedByParamRequestValidator : AbstractValidator<GetSalePaginatedByParamRequest>
{
    public GetSalePaginatedByParamRequestValidator()
    {
        RuleFor(x => x.PageIndex)
            .GreaterThan(0)
            .WithMessage("Page must be greater than or equal to 0");
        
        RuleFor(x => x.PageSize)
            .GreaterThan(0)
            .WithMessage("Page size must be greater than or equal to 0");
    }
}