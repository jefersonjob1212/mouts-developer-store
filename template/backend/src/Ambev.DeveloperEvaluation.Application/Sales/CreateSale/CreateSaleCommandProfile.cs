using Ambev.DeveloperEvaluation.Domain;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Profile for mapping class for CreateSaleCommand
/// </summary>
public class CreateSaleCommandProfile : Profile
{
    public CreateSaleCommandProfile()
    {
        CreateMap<CreateSaleCommand, Sale>()
            .ForMember(d => d.Items, 
                opt => opt.Ignore());
        
        CreateMap<SaleItem, CreateSaleItemResult>()
            .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.Name));
        CreateMap<Sale, CreateSaleResult>()
            .ForMember(dest => dest.ClientName,
                opt => opt.MapFrom(src =>
                    src.Client as IndividualClient != null
                        ? ((IndividualClient)src.Client).Name
                        : ((CompanyClient)src.Client).TradeName))
            .ForMember(dest => dest.SubsidiaryName,
                opt => opt.MapFrom(src => src.Subsidiary.TradeName));
    }
}