using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Subsidiaries.GetSubsidiary;

public class GetSubsidiaryByIdRequestValidator : AbstractValidator<GetSubsidiaryByIdRequest>
{
    public GetSubsidiaryByIdRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("ID is required");
    }
}