using Ambev.DeveloperEvaluation.Domain;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Filters;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSalePaginatedByParam;

public class GetSalePaginatedByParamProfile : Profile
{
    public GetSalePaginatedByParamProfile()
    {
        CreateMap<GetSalePaginatedByParamCommand, SaleFilter>();
        CreateMap<Sale, GetSalePaginatedByParamResult>()
            .ForMember(dest => dest.TotalValues, 
                opt => opt.MapFrom(src => src.Items.Sum(x => x.Total)))
            .ForMember(dest => dest.ClientName,
                opt => opt.MapFrom(src =>
                    src.Client as IndividualClient != null
                        ? ((IndividualClient)src.Client).Name
                        : ((CompanyClient)src.Client).TradeName))
            .ForMember(dest => dest.SubsidiaryName,
                opt => opt.MapFrom(src => src.Subsidiary.TradeName));;
    }
}