using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Sale : BaseEntity
{
    public long Number { get; set; }
    public DateTime Date { get; set; }
    
    public Guid ClientId { get; set; }

    public Client Client { get; set; } = null!;
    
    public Guid SubsidiaryId { get; set; }

    public Subsidiary Subsidiary { get; set; } = null!;
    public SaleStatus Status { get; set; }

    public virtual ICollection<SaleItem> Items { get; set; } = new List<SaleItem>();

    public Sale()
    {
    }

    public Sale(long number, DateTime date, Guid clientId, Guid subsidiaryId)
    {
        Number = number;
        Date = date;
        ClientId = clientId;
        SubsidiaryId = subsidiaryId;
        Status = SaleStatus.Created;
    }

    public void AddItem(SaleItem item)
    {
        Items.Add(item);
    }
    
    public void UpdateItems(IEnumerable<SaleItem> newItems)
    {
        var incomingProductIds = newItems
            .Select(x => x.ProductId)
            .ToHashSet();


        var itemsToRemove = Items
            .Where(x => !incomingProductIds.Contains(x.ProductId))
            .ToList();

        foreach (var item in itemsToRemove)
        {
            Items.Remove(item);
        }

        foreach (var newItem in newItems)
        {
            var existingItem = Items
                .FirstOrDefault(x => x.ProductId == newItem.ProductId);

            if (existingItem is null)
            {
                AddItem(newItem);
                continue;
            }
            
            existingItem.Update(
                newItem.Quantity,
                newItem.UnitPrice
            );
        }
    }
    
    public void CancelRemovedItems(IEnumerable<Guid> productIds)
    {
        var ids = productIds.ToHashSet();

        foreach (var item in Items.Where(x => x.Status == SaleItemStatus.Active))
        {
            if (!ids.Contains(item.ProductId))
                item.Cancel();
        }
    }
    
    public bool HasActiveItem(Guid productId)
    {
        return Items.Any(x =>
            x.ProductId == productId &&
            x.Status == SaleItemStatus.Active);
    }
}