using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Factories;
using Ambev.DeveloperEvaluation.Domain.Interfaces;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
{
    private readonly ILogger<UpdateSaleHandler> _logger;
    private readonly ISaleRepository _saleRepository;
    private readonly ISaleItemFactory _saleItemFactory;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateSaleHandler(
        ILogger<UpdateSaleHandler> logger, 
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

    public async Task<UpdateSaleResult> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
    {
        var saleValidator = new UpdateSaleCommandValidator();
        var saleValidationResult = await saleValidator.ValidateAsync(request, cancellationToken);
        if (!saleValidationResult.IsValid)
        {
            _logger.LogError("Error creating a sale: {0}", saleValidationResult.Errors);
            throw new ValidationException(saleValidationResult.Errors);
        }
        
        var sale = await _saleRepository
            .GetByIdWithItemsAsync(request.Id, cancellationToken);
        
        if (sale is null)
        {
            _logger.LogError("Error updating a sale. Sale not found with id {0}", request.Id);
            throw new KeyNotFoundException("Sale not found");
        }
        
        sale.CancelRemovedItems(
            request.Items.Select(x => x.ProductId));

        foreach(var item in request.Items)
        {
            var existingItem = sale.Items
                .FirstOrDefault(x => x.ProductId == item.ProductId 
                                     && x.Status == SaleItemStatus.Active);
            
            if (existingItem is not null)
            {
                existingItem.UpdateQuantity(item.Quantity);
                continue;
            }
            
            var newItem = await _saleItemFactory.CreateAsync(
                item.ProductId,
                item.Quantity,
                cancellationToken);

            sale.AddItem(newItem);
        }
        
        await _saleRepository.UpdateAsync(sale, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);
        _logger.LogInformation("Sale updated successfully");
        return _mapper.Map<UpdateSaleResult>(sale);
    }
}