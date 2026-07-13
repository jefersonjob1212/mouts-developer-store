using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Subsidiaries.GetSubsidiaryById;

public class GetSubsidiaryByIdProfile : Profile
{
    public GetSubsidiaryByIdProfile()
    {
        CreateMap<Subsidiary, GetSubsidiaryByIdResult>();
    }
}