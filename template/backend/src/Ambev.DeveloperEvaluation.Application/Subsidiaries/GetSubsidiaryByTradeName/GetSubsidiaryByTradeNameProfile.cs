using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Subsidiaries.GetSubsidiaryByTradeName;

public class GetSubsidiaryByTradeNameProfile : Profile
{
    public GetSubsidiaryByTradeNameProfile()
    {
        CreateMap<Subsidiary, GetSubsidiaryByTradeNameResult>();
    }
}