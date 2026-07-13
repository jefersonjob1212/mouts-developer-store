using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Clients.GetCompanyClientsByLegalName;

/// <summary>
/// Validator for GetCompanyClientByCnpjValidator
/// </summary>
public class GetCompanyClientsByLegalNameValidator : AbstractValidator<GetCompanyClientsByLegalNameCommand>
{
    /// <summary>
    /// Initialize new instance of GetClientByEmailValidator
    /// </summary>
    public GetCompanyClientsByLegalNameValidator()
    {
        RuleFor(x => x.LegalName)
            .NotEmpty()
            .WithMessage("Legal name is required.");
    }
}