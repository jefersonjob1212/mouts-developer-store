using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class CompanyClientValidator : AbstractValidator<CompanyClient>
{
    public CompanyClientValidator()
    {
        RuleFor(client => client.Cnpj).SetValidator(new CnpjValidator());
        
        RuleFor(client => client.LegalName)
            .NotEmpty()
            .WithMessage("Legal Name is required");
        
        RuleFor(client => client.TradeName)
            .NotEmpty()
            .WithMessage("Trade Name is required");
    }
}