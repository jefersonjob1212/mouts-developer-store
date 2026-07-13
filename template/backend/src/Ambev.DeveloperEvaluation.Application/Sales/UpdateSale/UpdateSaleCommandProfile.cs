using Ambev.DeveloperEvaluation.Domain;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Profile for mapping class for UpdateSaleCommand
/// </summary>
public class UpdateSaleCommandProfile : Profile
{
    public UpdateSaleCommandProfile()
    {
        CreateMap<UpdateSaleCommand, Sale>()
            .ForMember(d => d.Items, 
                opt => opt.Ignore());
        
        CreateMap<SaleItem, UpdateSaleItemResult>()
            .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.Name));
        CreateMap<Sale, UpdateSaleResult>()
            .ForMember(dest => dest.ClientName,
                opt => opt.MapFrom(src =>
                    src.Client as IndividualClient != null
                        ? ((IndividualClient)src.Client).Name
                        : ((CompanyClient)src.Client).TradeName))
            .ForMember(dest => dest.SubsidiaryName,
                opt => opt.MapFrom(src => src.Subsidiary.TradeName));
    }
}