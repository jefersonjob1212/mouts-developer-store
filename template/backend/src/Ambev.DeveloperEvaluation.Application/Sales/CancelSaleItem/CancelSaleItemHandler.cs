using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Factories;
using Ambev.DeveloperEvaluation.Domain.Interfaces;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem;

public class CancelSaleItemHandler : IRequestHandler<CancelSaleItemCommand, CancelSaleItemResult>
{
    private readonly ILogger<CancelSaleItemHandler> _logger;
    private readonly ISaleRepository _saleRepository;
    private readonly ISaleItemFactory _saleItemFactory;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CancelSaleItemHandler(
        ILogger<CancelSaleItemHandler> logger, 
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

    public async Task<CancelSaleItemResult> Handle(CancelSaleItemCommand request, CancellationToken cancellationToken)
    {
        var saleItemValidator = new CancelSaleItemCommandValidator();
        var saleItemValidationResult = await saleItemValidator.ValidateAsync(request, cancellationToken);
        if (!saleItemValidationResult.IsValid)
        {
            _logger.LogError("Error canceling a sale: {0}", saleItemValidationResult.Errors);
            throw new ValidationException(saleItemValidationResult.Errors);
        }
        
        var saleItem = await _saleRepository
            .GetItemById(request.Id, cancellationToken);
        
        if (saleItem is null)
        {
            _logger.LogError("Error canceling a sale. Sale not found with id {0}", request.Id);
            throw new KeyNotFoundException("Sale not found");
        }

        saleItem.Status = SaleItemStatus.Canceled;
        await _saleRepository.UpdateItemAsync(saleItem, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);
        _logger.LogInformation("Sale item canceled successfully");
        return _mapper.Map<CancelSaleItemResult>(saleItem);
    }
}