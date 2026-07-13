using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Subsidiaries.GetSubsidiary;

public class GetSubsidiaryByCnpjRequestValidator : AbstractValidator<GetSubsidiaryByCnpjRequest>
{
    public GetSubsidiaryByCnpjRequestValidator()
    {
        RuleFor(x => x.Cnpj).SetValidator(new CnpjValidator());
    }
}