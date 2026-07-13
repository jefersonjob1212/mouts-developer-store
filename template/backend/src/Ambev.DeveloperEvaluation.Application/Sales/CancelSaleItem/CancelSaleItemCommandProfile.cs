using Ambev.DeveloperEvaluation.Domain;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem;

/// <summary>
/// Profile for mapping class for CancelSaleItemCommand
/// </summary>
public class CancelSaleItemCommandProfile : Profile
{
    public CancelSaleItemCommandProfile()
    {
        CreateMap<SaleItem, CancelSaleItemResult>()
            .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.Name));
    }
}