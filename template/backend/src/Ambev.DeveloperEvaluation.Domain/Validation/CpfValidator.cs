using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class CpfValidator : AbstractValidator<string>
{
    public CpfValidator()
    {
        RuleFor(cpf => cpf)
            .NotEmpty()
            .WithMessage("The CPF cannot be empty.")
            .Length(11)
            .WithMessage("The CPF must have 11 digits.")
            .Must(BeAValidCpf)
            .WithMessage("The CPF is invalid.");
    }
    
    private static bool BeAValidCpf(string? cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
            return false;

        cpf = new string(cpf.Where(char.IsDigit).ToArray());

        if (cpf.Length != 11)
            return false;

        if (cpf.All(c => c == cpf[0]))
            return false;

        var numbers = cpf.Select(c => c - '0').ToArray();

        var sum = 0;
        for (int i = 0; i < 9; i++)
        {
            sum += numbers[i] * (10 - i);
        }

        var remainder = sum % 11;
        var firstDigit = remainder < 2 ? 0 : 11 - remainder;

        if (numbers[9] != firstDigit)
            return false;

        sum = 0;
        for (int i = 0; i < 10; i++)
        {
            sum += numbers[i] * (11 - i);
        }

        remainder = sum % 11;
        var secondDigit = remainder < 2 ? 0 : 11 - remainder;

        return numbers[10] == secondDigit;
    }
}