using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProductsByName;

/// <summary>
/// Handler for processing GetProductsByNameCommand requests
/// </summary>
public class GetProductsByNameHandler : IRequestHandler<GetProductsByNameCommand, IEnumerable<GetProductByNameResult>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initialize a new instance of GetProductsByNameHandler
    /// </summary>
    /// <param name="productRepository"></param>
    /// <param name="mapper"></param>
    public GetProductsByNameHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetProductsByNameCommand request
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="ValidationException"></exception>
    /// <exception cref="KeyNotFoundException"></exception>
    public async Task<IEnumerable<GetProductByNameResult>> Handle(GetProductsByNameCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetProductsByNameValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        var product = await _productRepository.GetByNameAsync(request.Name);
        if (!product.Any())
            throw new KeyNotFoundException($"Products with name {request.Name} was not found");
        
        return _mapper.Map<IEnumerable<GetProductByNameResult>>(product);
    }
}