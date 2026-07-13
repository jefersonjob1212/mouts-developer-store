using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Subsidiaries.GetSubsidiaryById;

/// <summary>
/// Validator for GetSubsidiaryByIdCommand
/// </summary>
public class GetSubsidiaryByIdValidator : AbstractValidator<GetSubsidiaryByIdCommand>
{
    /// <summary>
    /// Initializes validation rules for GetSubsidiaryByIdCommand
    /// </summary>
    public GetSubsidiaryByIdValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .NotEmpty()
            .WithMessage("Id is required");
    }
}