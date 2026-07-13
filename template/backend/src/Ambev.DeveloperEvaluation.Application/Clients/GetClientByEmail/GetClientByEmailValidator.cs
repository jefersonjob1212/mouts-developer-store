using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Clients.GetClientByEmail;

/// <summary>
/// Validator for GetClientByEmailValidator
/// </summary>
public class GetClientByEmailValidator : AbstractValidator<GetClientByEmailCommand>
{
    /// <summary>
    /// Initialize new instance of GetClientByEmailValidator
    /// </summary>
    public GetClientByEmailValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Client Email is required.");
    }
}