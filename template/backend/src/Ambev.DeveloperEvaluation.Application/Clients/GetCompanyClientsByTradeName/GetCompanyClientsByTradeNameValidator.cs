using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Clients.GetCompanyClientsByTradeName;

/// <summary>
/// Validator for GetCompanyClientByCnpjValidator
/// </summary>
public class GetCompanyClientsByTradeNameValidator : AbstractValidator<GetCompanyClientsByTradeNameCommand>
{
    /// <summary>
    /// Initialize new instance of GetClientByEmailValidator
    /// </summary>
    public GetCompanyClientsByTradeNameValidator()
    {
        RuleFor(x => x.TradeName)
            .NotEmpty()
            .WithMessage("Trade name is required.");
    }
}