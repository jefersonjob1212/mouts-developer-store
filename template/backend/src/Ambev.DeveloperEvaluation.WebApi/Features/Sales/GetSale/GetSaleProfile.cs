using Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;
using Ambev.DeveloperEvaluation.Application.Sales.GetSaleItem;
using Ambev.DeveloperEvaluation.Application.Sales.GetSalePaginatedByParam;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

public class GetSaleProfile : Profile
{
    public GetSaleProfile()
    {
        CreateMap<GetSaleByIdRequest, GetSaleByIdCommand>();
        CreateMap<GetSalePaginatedByParamRequest, GetSalePaginatedByParamCommand>();
        CreateMap<GetSaleByIdResult, GetSaleResponse>();
        CreateMap<GetSalePaginatedByParamResult, GetSaleResponse>();
        CreateMap<GetSaleItemResult, GetSaleItemResponse>();
        CreateMap<GetSalePaginatedResult, GetSalePaginatedResponse>();
    }
}