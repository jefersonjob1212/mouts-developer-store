using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Subsidiaries.GetSubsidiaryByLegalName;

/// <summary>
/// Validator for GetSubsidiaryByLegalNameCommand
/// </summary>
public class GetSubsidiaryByLegalNameValidator : AbstractValidator<GetSubsidiaryByLegalNameCommand>
{
    /// <summary>
    /// Initializes validation rules for GetSubsidiaryByLegalNameCommand
    /// </summary>
    public GetSubsidiaryByLegalNameValidator()
    {
        RuleFor(x => x.LegalName)
            .NotEmpty()
            .WithMessage("Legal name cannot be empty");
    }
}