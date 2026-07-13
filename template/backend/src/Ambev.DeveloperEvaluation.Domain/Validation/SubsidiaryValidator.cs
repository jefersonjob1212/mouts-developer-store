using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class SubsidiaryValidator : AbstractValidator<Subsidiary>
{
    public SubsidiaryValidator()
    {
        RuleFor(subsidiary => subsidiary.Cnpj).SetValidator(new CnpjValidator());
        RuleFor(subsidiary => subsidiary.TradeName)
            .NotEmpty()
            .WithMessage("Trade Name is required");
        
        RuleFor(subsidiary => subsidiary.City)
            .NotEmpty()
            .WithMessage("City is required");
        
        RuleFor(subsidiary => subsidiary.State)
            .NotEmpty()
            .WithMessage("State is required");
        
        RuleFor(subsidiary => subsidiary.LegalName)
            .NotEmpty()
            .WithMessage("Legal Name is required");
    }
}