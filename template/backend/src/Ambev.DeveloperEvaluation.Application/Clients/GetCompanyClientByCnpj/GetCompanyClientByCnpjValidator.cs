using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Clients.GetCompanyClientByCnpj;

/// <summary>
/// Validator for GetCompanyClientByCnpjValidator
/// </summary>
public class GetCompanyClientByCnpjValidator : AbstractValidator<GetCompanyClientByCnpjCommand>
{
    /// <summary>
    /// Initialize new instance of GetClientByEmailValidator
    /// </summary>
    public GetCompanyClientByCnpjValidator()
    {
        RuleFor(x => x.Cnpj)
            .NotEmpty()
            .WithMessage("CNPJ is required.");
    }
}