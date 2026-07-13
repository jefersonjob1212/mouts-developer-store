using Ambev.DeveloperEvaluation.Application.Products.GetProductByCode;
using Ambev.DeveloperEvaluation.Application.Products.GetProductById;
using Ambev.DeveloperEvaluation.Application.Products.GetProductsByName;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;

public class GetProductProfile : Profile
{
    public GetProductProfile()
    {
        CreateMap<GetProductByIdRequest, GetProductByIdCommand>();
        CreateMap<GetProductByCodeRequest, GetProductByCodeCommand>();
        CreateMap<GetProductByNameRequest, GetProductsByNameCommand>();
        CreateMap<GetProductByIdResult, GetProductResponse>();
        CreateMap<GetProductByCodeResult, GetProductResponse>();
        CreateMap<GetProductByNameResult, GetProductResponse>();
    }
}