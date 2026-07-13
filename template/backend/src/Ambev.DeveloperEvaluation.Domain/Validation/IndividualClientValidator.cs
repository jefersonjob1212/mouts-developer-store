using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class IndividualClientValidator : AbstractValidator<IndividualClient>
{
    public IndividualClientValidator()
    {
        RuleFor(client => client.Cpf).SetValidator(new CpfValidator());
        
        RuleFor(client => client.Name)
            .NotEmpty()
            .WithMessage("Name is required");
        
        RuleFor(client => client.Gender)
            .NotNull()
            .WithMessage("Gender is required");
        
        RuleFor(client => client.BornDate)
            .NotNull()
            .WithMessage("The born date is required.")
            .Must(BeA18YearsOfAgeOrOlder)
            .WithMessage("Registration is not permitted for individuals under 18.");
    }

    private static bool BeA18YearsOfAgeOrOlder(DateTime date)
    {
        var today = DateTime.Today;
        var age = today.Year - date.Year;
        return age >= 18;
    }
}