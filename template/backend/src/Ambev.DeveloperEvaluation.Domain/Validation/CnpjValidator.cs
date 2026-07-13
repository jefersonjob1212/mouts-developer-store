using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class CnpjValidator : AbstractValidator<string>
{
    public CnpjValidator()
    {
        RuleFor(cnpj => cnpj)
            .NotEmpty()
            .WithMessage("The CNPJ cannot be empty.")
            .Length(14)
            .WithMessage("The CNPJ must contain 14 digits.")
            .Must(BeAValidCnpj)
            .WithMessage("This is not a valid CNPJ.");;
    }
    
    private static bool BeAValidCnpj(string? cnpj)
    {
        if (string.IsNullOrWhiteSpace(cnpj))
            return false;

        cnpj = new string(cnpj.Where(char.IsDigit).ToArray());

        if (cnpj.Length != 14)
            return false;

        if (cnpj.All(c => c == cnpj[0]))
            return false;

        Span<int> numbers = stackalloc int[14];

        for (int i = 0; i < 14; i++)
            numbers[i] = cnpj[i] - '0';

        ReadOnlySpan<int> firstWeights = stackalloc int[]
        {
            5,4,3,2,9,8,7,6,5,4,3,2
        };

        ReadOnlySpan<int> secondWeights = stackalloc int[]
        {
            6,5,4,3,2,9,8,7,6,5,4,3,2
        };

        var sum = 0;
        for (int i = 0; i < 12; i++)
            sum += numbers[i] * firstWeights[i];

        var remainder = sum % 11;
        var firstDigit = remainder < 2 ? 0 : 11 - remainder;

        if (numbers[12] != firstDigit)
            return false;

        sum = 0;
        for (int i = 0; i < 13; i++)
            sum += numbers[i] * secondWeights[i];

        remainder = sum % 11;
        var secondDigit = remainder < 2 ? 0 : 11 - remainder;

        return numbers[13] == secondDigit;
    }
}