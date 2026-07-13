using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Clients.GetIndividualClientByCpf;

/// <summary>
/// Validator for GetIndividualClientByCpfValidator
/// </summary>
public class GetIndividualClientByCpfValidator : AbstractValidator<GetIndividualClientByCpfCommand>
{
    /// <summary>
    /// Initialize new instance of GetClientByEmailValidator
    /// </summary>
    public GetIndividualClientByCpfValidator()
    {
        RuleFor(x => x.Cpf)
            .NotEmpty()
            .WithMessage("CPF is required.");
    }
}