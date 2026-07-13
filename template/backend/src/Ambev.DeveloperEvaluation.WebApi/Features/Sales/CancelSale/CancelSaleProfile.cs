using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale;

public class CancelSaleProfile : Profile
{
    public CancelSaleProfile()
    {
        CreateMap<CancelSaleRequest, CancelSaleCommand>();
        CreateMap<CancelSaleItemRequest, CancelSaleItemCommand>();
        CreateMap<CancelSaleItemResult, CancelSaleItemResponse>();
        CreateMap<CancelSaleResult, CancelSaleResponse>();
    }
}
