using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

public class SaleTests
{
    [Fact]
    public void Constructor_Should_Create_Sale_With_Created_Status()
    {
        // Arrange
        var number = 12345;
        var date = DateTime.UtcNow;
        var clientId = Guid.NewGuid();
        var subsidiaryId = Guid.NewGuid();


        // Act
        var sale = new Sale(
            number,
            date,
            clientId,
            subsidiaryId);


        // Assert
        sale.Number.Should()
            .Be(number);

        sale.Date.Should()
            .Be(date);

        sale.ClientId.Should()
            .Be(clientId);

        sale.SubsidiaryId.Should()
            .Be(subsidiaryId);

        sale.Status.Should()
            .Be(SaleStatus.Created);

        sale.Items.Should()
            .BeEmpty();
    }


    [Fact]
    public void AddItem_Should_Add_New_Item_To_Sale()
    {
        // Arrange
        var sale = new SaleTestData().Generate();

        var item = new SaleItemTestData().Generate();


        // Act
        sale.AddItem(item);


        // Assert
        sale.Items.Should()
            .ContainSingle();

        sale.Items.First()
            .Should()
            .Be(item);
    }


    [Fact]
    public void UpdateItems_Should_Remove_Items_Not_In_New_List()
    {
        // Arrange
        var sale = new SaleTestData().Generate();

        var itemToRemove = new SaleItemTestData().Generate();
        var itemToKeep = new SaleItemTestData().Generate();


        sale.AddItem(itemToRemove);
        sale.AddItem(itemToKeep);


        var newItems = new[]
        {
            new SaleItemTestData()
                .RuleFor(
                    x => x.ProductId,
                    _ => itemToKeep.ProductId).Generate()
        };


        // Act
        sale.UpdateItems(newItems);


        // Assert
        sale.Items.Should()
            .ContainSingle();

        sale.Items.First()
            .ProductId.Should()
            .Be(itemToKeep.ProductId);
    }


    [Fact]
    public void UpdateItems_Should_Add_New_Item_When_Product_Does_Not_Exist()
    {
        // Arrange
        var sale = new SaleTestData().Generate();

        var existingItem = new SaleItemTestData().Generate();

        sale.AddItem(existingItem);


        var newItem = new SaleItemTestData().Generate();


        // Act
        sale.UpdateItems(
            new[]
            {
                existingItem,
                newItem
            });


        // Assert
        sale.Items.Should()
            .HaveCount(2);

        sale.Items.Should()
            .Contain(x =>
                x.ProductId == newItem.ProductId);
    }


    [Fact]
    public void UpdateItems_Should_Update_Existing_Item()
    {
        // Arrange
        var sale = new SaleTestData().Generate();

        var productId = Guid.NewGuid();

        var existingItem = new SaleItemTestData()
            .RuleFor(
                x => x.ProductId,
                _ => productId)
            .Generate();

        sale.AddItem(existingItem);


        var updatedItem = new SaleItemTestData()
            .RuleFor(
                x => x.ProductId,
                _ => productId)
            .Generate();
        
        // Act
        sale.UpdateItems(
            new[]
            {
                updatedItem
            });

        // Assert
        sale.Items.Should()
            .ContainSingle();

        var item = sale.Items.First();

        item.Quantity.Should()
            .Be(updatedItem.Quantity);

        item.UnitPrice.Should()
            .Be(updatedItem.UnitPrice);
    }
    
    [Fact]
    public void CancelRemovedItems_Should_Cancel_Only_Active_Items()
    {
        // Arrange
        var sale = new SaleTestData().Generate();

        var activeItem = new SaleItemTestData().Generate();

        var canceledItem = new SaleItemTestData().Generate();

        canceledItem.Cancel();
        
        sale.AddItem(activeItem);
        sale.AddItem(canceledItem);
        
        var productsToKeep = new[]
        {
            canceledItem.ProductId
        };
        
        // Act
        sale.CancelRemovedItems(productsToKeep);
        
        // Assert
        activeItem.Status.Should()
            .Be(SaleItemStatus.Canceled);
        
        canceledItem.Status.Should()
            .Be(SaleItemStatus.Canceled);
    }


    [Fact]
    public void HasActiveItem_Should_Return_True_When_Item_Is_Active()
    {
        // Arrange
        var sale = new SaleTestData().Generate();

        var item = new SaleItemTestData().Generate();

        sale.AddItem(item);
        
        // Act
        var result = sale.HasActiveItem(item.ProductId);

        // Assert
        result.Should()
            .BeTrue();
    }


    [Fact]
    public void HasActiveItem_Should_Return_False_When_Item_Is_Canceled()
    {
        // Arrange
        var sale = new SaleTestData().Generate();

        var item = new SaleItemTestData().Generate();

        item.Cancel();

        sale.AddItem(item);
        
        // Act
        var result = sale.HasActiveItem(item.ProductId);
        
        // Assert
        result.Should()
            .BeFalse();
    }

}