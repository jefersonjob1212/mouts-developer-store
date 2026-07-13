using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleItem;

public class GetSaleItemProfile : Profile
{
    public GetSaleItemProfile()
    {
        CreateMap<SaleItem, GetSaleItemResult>()
            .ForMember(x => x.ProductName, y => y.MapFrom(x => x.Product.Name));
    }
}