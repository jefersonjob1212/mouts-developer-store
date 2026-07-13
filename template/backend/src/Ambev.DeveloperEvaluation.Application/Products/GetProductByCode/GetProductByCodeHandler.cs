using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProductByCode;

/// <summary>
/// Handler for processing GetProductByCodeCommand requests
/// </summary>
public class GetProductByCodeHandler : IRequestHandler<GetProductByCodeCommand, GetProductByCodeResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initialize a new instance of GetProductByCodeHandler
    /// </summary>
    /// <param name="productRepository"></param>
    /// <param name="mapper"></param>
    public GetProductByCodeHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetProductByCodeCommand request
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="ValidationException"></exception>
    public async Task<GetProductByCodeResult> Handle(GetProductByCodeCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetProductByCodeValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        var product = await _productRepository.GetByCodeAsync(request.Code, cancellationToken);
        return _mapper.Map<GetProductByCodeResult>(product);
    }
}