using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Clients.GetClientById;

/// <summary>
/// Validator for GetClientByIdCommand
/// </summary>
public class GetClientByIdValidator : AbstractValidator<GetClientByIdCommand>
{
    /// <summary>
    /// Initialize new instance of GetClientByIdValidator
    /// </summary>
    public GetClientByIdValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Client ID is required.");
    }
}