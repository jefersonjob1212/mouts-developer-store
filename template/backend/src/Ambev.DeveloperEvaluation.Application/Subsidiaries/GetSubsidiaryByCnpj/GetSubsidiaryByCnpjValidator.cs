using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Subsidiaries.GetSubsidiaryByCnpj;

/// <summary>
/// Validator for GetSubsidiaryByCnpjCommand
/// </summary>
public class GetSubsidiaryByCnpjValidator : AbstractValidator<GetSubsidiaryByCnpjCommand>
{
    /// <summary>
    /// Initializes validation rules for GetSubsidiaryByCnpjCommand
    /// </summary>
    public GetSubsidiaryByCnpjValidator()
    {
        RuleFor(x => x.Cnpj).SetValidator(new CnpjValidator());
    }
}