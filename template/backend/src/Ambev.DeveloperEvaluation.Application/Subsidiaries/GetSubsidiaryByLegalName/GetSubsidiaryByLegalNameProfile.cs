using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Subsidiaries.GetSubsidiaryByLegalName;

public class GetSubsidiaryByLegalNameProfile : Profile
{
    public GetSubsidiaryByLegalNameProfile()
    {
        CreateMap<Subsidiary, GetSubsidiaryByLegalNameResult>();
    }
}