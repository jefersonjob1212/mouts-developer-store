using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Subsidiaries.GetSubsidiaryByTradeName;

/// <summary>
/// Validator for GetSubsidiaryByTradeNameCommand
/// </summary>
public class GetSubsidiaryByTradeNameValidator : AbstractValidator<GetSubsidiaryByTradeNameCommand>
{
    /// <summary>
    /// Initializes validation rules for GetSubsidiaryByTradeNameCommand
    /// </summary>
    public GetSubsidiaryByTradeNameValidator()
    {
        RuleFor(x => x.TradeName)
            .NotEmpty()
            .WithMessage("Trade name cannot be empty");
    }
}