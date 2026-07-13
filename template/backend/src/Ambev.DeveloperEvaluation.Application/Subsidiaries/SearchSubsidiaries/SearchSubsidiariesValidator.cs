using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Subsidiaries.SearchSubsidiaries;

/// <summary>
/// Validator for GetCompanySubsidiariesByTermValidator
/// </summary>
public class SearchSubsidiariesValidator : AbstractValidator<SearchSubsidiariesCommand>
{
    /// <summary>
    /// Initialize new instance of GetClientByEmailValidator
    /// </summary>
    public SearchSubsidiariesValidator()
    {
        RuleFor(x => x.Term)
            .NotEmpty()
            .WithMessage("Term is required.");
    }
}