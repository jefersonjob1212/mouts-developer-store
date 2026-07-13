using Ambev.DeveloperEvaluation.Domain.Common;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class ClientValidator : AbstractValidator<Client>
{
    public ClientValidator()
    {
        RuleFor(client => client.Email).SetValidator(new EmailValidator());
        RuleFor(client => client.PhoneNumber).SetValidator(new PhoneValidator());
        RuleFor(client => client.Address)
            .NotEmpty()
            .WithMessage("The full address cannot be empty.");
    }
}