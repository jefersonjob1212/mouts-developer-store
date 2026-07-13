using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Clients.SearchClients;

/// <summary>
/// Validator for GetCompanyClientByCnpjValidator
/// </summary>
public class SearchClientsValidator : AbstractValidator<SearchClientsCommand>
{
    /// <summary>
    /// Initialize new instance of GetClientByEmailValidator
    /// </summary>
    public SearchClientsValidator()
    {
        RuleFor(x => x.Term)
            .NotEmpty()
            .WithMessage("Term is required.");
    }
}