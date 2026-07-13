using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProductById;

/// <summary>
/// Handler for processing GetProductByIdCommand requests
/// </summary>
public class GetProductByIdHandler : IRequestHandler<GetProductByIdCommand, GetProductByIdResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initialize a new instance of GetProductByIdHandler
    /// </summary>
    /// <param name="productRepository"></param>
    /// <param name="mapper"></param>
    public GetProductByIdHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetProductByIdCommand request
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="ValidationException"></exception>
    public async Task<GetProductByIdResult> Handle(GetProductByIdCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetProductByIdValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);
        if (product is null)
            throw new KeyNotFoundException($"Product with ID {request.Id} was not found");
        
        return _mapper.Map<GetProductByIdResult>(product);
    }
}