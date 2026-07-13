using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProductByCode;

/// <summary>
/// Profile for mapping between Product entity and GetProductByCode responses
/// </summary>
public class GetProductByCodeProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetProductsByCode operation
    /// </summary>
    public GetProductByCodeProfile()
    {
        CreateMap<Product, GetProductByCodeResult>();
    }
}