using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Subsidiaries.GetSubsidiary;

public class GetSubsidiaryByLegalNameRequestValidator : AbstractValidator<GetSubsidiaryByLegalNameRequest>
{
    public GetSubsidiaryByLegalNameRequestValidator()
    {
        RuleFor(x => x.LegalName)
            .NotEmpty()
            .WithMessage("Legal name is required");
    }
}