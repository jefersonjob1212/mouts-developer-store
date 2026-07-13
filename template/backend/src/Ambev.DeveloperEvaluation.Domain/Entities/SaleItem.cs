using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class SaleItem : BaseEntity
{
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
    
    public decimal UnitPrice { get; private set; }
    public int Quantity { get; set; }
    public decimal Discount { get; set; }
    public decimal Total { get; private set; }
    public SaleItemStatus Status { get; set; }

    public Guid SaleId { get; set; }
    public virtual Sale Sale { get; set; }

    public SaleItem(Guid productId, int quantity, decimal unitPrice)
    {
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Status = SaleItemStatus.Active;
        
        CalculateTotal();
    }

    private void CalculateTotal()
    {
        Total = UnitPrice * Quantity;
        Discount = Quantity switch
        {
            > 4 and < 10 => Total * 10 / 100,
            >= 10 and <= 20 => Total * 20 / 100,
            _ => 0
        };

        if (Discount > 0)
            Total -= Discount;
    }
    
    public void Update(int quantity, decimal unitPrice)
    {
        Quantity = quantity;
        UnitPrice = unitPrice;

        CalculateTotal();
    }
    
    public void Cancel()
    {
        Status = SaleItemStatus.Canceled;
    }
    
    public void UpdateQuantity(int quantity)
    {
        if (Status == SaleItemStatus.Canceled)
            throw new DomainException(
                "Cannot update a canceled item");

        Quantity = quantity;
        CalculateTotal();
    }
}