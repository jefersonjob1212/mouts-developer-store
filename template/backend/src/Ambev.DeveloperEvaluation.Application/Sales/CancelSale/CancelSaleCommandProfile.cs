using Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem;
using Ambev.DeveloperEvaluation.Domain;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

/// <summary>
/// Profile for mapping class for CancelSaleCommand
/// </summary>
public class CancelSaleCommandProfile : Profile
{
    public CancelSaleCommandProfile()
    {
        CreateMap<CancelSaleCommand, Sale>()
            .ForMember(d => d.Items, 
                opt => opt.Ignore());
        
        CreateMap<SaleItem, CancelSaleItemResult>()
            .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.Name));
        CreateMap<Sale, CancelSaleResult>()
            .ForMember(dest => dest.ClientName,
                opt => opt.MapFrom(src =>
                    src.Client as IndividualClient != null
                        ? ((IndividualClient)src.Client).Name
                        : ((CompanyClient)src.Client).TradeName))
            .ForMember(dest => dest.SubsidiaryName,
                opt => opt.MapFrom(src => src.Subsidiary.TradeName));
    }
}