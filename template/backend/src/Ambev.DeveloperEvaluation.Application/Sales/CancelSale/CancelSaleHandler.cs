using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Factories;
using Ambev.DeveloperEvaluation.Domain.Interfaces;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

public class CancelSaleHandler : IRequestHandler<CancelSaleCommand, CancelSaleResult>
{
    private readonly ILogger<CancelSaleHandler> _logger;
    private readonly ISaleRepository _saleRepository;
    private readonly ISaleItemFactory _saleItemFactory;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CancelSaleHandler(
        ILogger<CancelSaleHandler> logger, 
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

    public async Task<CancelSaleResult> Handle(CancelSaleCommand request, CancellationToken cancellationToken)
    {
        var saleValidator = new CancelSaleCommandValidator();
        var saleValidationResult = await saleValidator.ValidateAsync(request, cancellationToken);
        if (!saleValidationResult.IsValid)
        {
            _logger.LogError("Error canceling a sale: {0}", saleValidationResult.Errors);
            throw new ValidationException(saleValidationResult.Errors);
        }
        
        var sale = await _saleRepository
            .GetByIdWithItemsAsync(request.Id, cancellationToken);
        
        if (sale is null)
        {
            _logger.LogError("Error canceling a sale. Sale not found with id {0}", request.Id);
            throw new KeyNotFoundException("Sale not found");
        }

        sale.Status = SaleStatus.Canceled;
        foreach (var saleItem in sale.Items)
        {
            saleItem.Cancel();
        }
        await _saleRepository.UpdateAsync(sale, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);
        _logger.LogInformation("Sale canceled successfully");
        return _mapper.Map<CancelSaleResult>(sale);
    }
}