using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Factories;
using Ambev.DeveloperEvaluation.Domain.Interfaces;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ILogger<CreateSaleHandler> _logger;
    private readonly ISaleRepository _saleRepository;
    private readonly ISaleItemFactory _saleItemFactory;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateSaleHandler(
        ILogger<CreateSaleHandler> logger, 
        ISaleRepository saleRepository, 
        ISaleItemFactory saleItemFactory,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _logger = logger;
        _saleRepository = saleRepository;
        _saleItemFactory = saleItemFactory;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CreateSaleResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        var saleValidator = new CreateSaleCommandValidator();
        var saleValidationResult = await saleValidator.ValidateAsync(request, cancellationToken);
        if (!saleValidationResult.IsValid)
        {
            _logger.LogError("Error creating a sale: {0}", saleValidationResult.Errors);
            throw new ValidationException(saleValidationResult.Errors);
        }
        
        var sale = _mapper.Map<Sale>(request);
        sale.Date = DateTime.UtcNow;
        foreach (var itemCommand in request.Items)
        {
            var item = await _saleItemFactory.CreateAsync(itemCommand.ProductId, itemCommand.Quantity, cancellationToken);
            sale.AddItem(item);
        }
        await _saleRepository.CreateAsync(sale, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);
        _logger.LogInformation("Sale created successfully");
        return _mapper.Map<CreateSaleResult>(sale);
    }
}