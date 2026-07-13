using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Clients.GetIndividualClientByName;

/// <summary>
/// Validator for GetIndividualClientByNameValidator
/// </summary>
public class GetIndividualClientByNameValidator : AbstractValidator<GetIndividualClientByNameCommand>
{
    /// <summary>
    /// Initialize new instance of GetClientByEmailValidator
    /// </summary>
    public GetIndividualClientByNameValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Client name is required.");
    }
}