using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Subsidiaries.GetSubsidiary;

public class GetSubsidiaryByTradeNameRequestValidator : AbstractValidator<GetSubsidiaryByTradeNameRequest>
{
    public GetSubsidiaryByTradeNameRequestValidator()
    {
        RuleFor(x => x.TradeName)
            .NotEmpty()
            .WithMessage("Trade name is required");
    }
}