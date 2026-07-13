using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProductsByName;

/// <summary>
/// Profile for mapping between Product entity and GetProductsByName responses
/// </summary>
public class GetProductsByNameProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetProductsByName operation
    /// </summary>
    public GetProductsByNameProfile()
    {
        CreateMap<Product, GetProductByNameResult>();
    }
}