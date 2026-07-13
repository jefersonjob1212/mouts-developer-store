using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProductById;

/// <summary>
/// Profile for mapping between Product entity and GetProductById responses
/// </summary>
public class GetProductByIdProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetProductById operation
    /// </summary>
    public GetProductByIdProfile()
    {
        CreateMap<Product, GetProductByIdResult>();
    }
}