using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

public class SaleItemTests
{
    [Fact]
    public void Constructor_Should_Create_Active_Item_And_Calculate_Total()
    {
        // Arrange
        var productId = Guid.NewGuid();


        // Act
        var item = new SaleItem(
            productId,
            2,
            100);


        // Assert
        item.ProductId.Should()
            .Be(productId);

        item.Quantity.Should()
            .Be(2);

        item.UnitPrice.Should()
            .Be(100);

        item.Status.Should()
            .Be(SaleItemStatus.Active);

        item.Discount.Should()
            .Be(0);

        item.Total.Should()
            .Be(200);
    }


    [Fact]
    public void Constructor_Should_Apply_10_Percent_Discount_When_Quantity_Is_Between_5_And_9()
    {
        // Arrange
        var item = new SaleItem(
            Guid.NewGuid(),
            5,
            100);


        // Act


        // Assert
        item.Discount.Should()
            .Be(50);

        item.Total.Should()
            .Be(450);
    }


    [Fact]
    public void Constructor_Should_Apply_20_Percent_Discount_When_Quantity_Is_Between_10_And_20()
    {
        // Arrange
        var item = new SaleItem(
            Guid.NewGuid(),
            10,
            100);


        // Assert
        item.Discount.Should()
            .Be(200);

        item.Total.Should()
            .Be(800);
    }


    [Fact]
    public void Update_Should_Update_Values_And_Recalculate_Total()
    {
        // Arrange
        var item = new SaleItem(
            Guid.NewGuid(),
            2,
            100);


        // Act
        item.Update(
            10,
            50);


        // Assert
        item.Quantity.Should()
            .Be(10);

        item.UnitPrice.Should()
            .Be(50);

        item.Discount.Should()
            .Be(100);

        item.Total.Should()
            .Be(400);
    }


    [Fact]
    public void Cancel_Should_Change_Status_To_Canceled()
    {
        // Arrange
        var item = new SaleItemTestData()
            .Generate();


        // Act
        item.Cancel();


        // Assert
        item.Status.Should()
            .Be(SaleItemStatus.Canceled);
    }


    [Fact]
    public void UpdateQuantity_Should_Update_Quantity_When_Item_Is_Active()
    {
        // Arrange
        var item = new SaleItem(
            Guid.NewGuid(),
            2,
            100);


        // Act
        item.UpdateQuantity(5);


        // Assert
        item.Quantity.Should()
            .Be(5);

        item.Discount.Should()
            .Be(50);

        item.Total.Should()
            .Be(450);
    }


    [Fact]
    public void UpdateQuantity_Should_Throw_Exception_When_Item_Is_Canceled()
    {
        // Arrange
        var item = new SaleItemTestData()
            .Generate();

        item.Cancel();


        // Act
        Action action = () =>
            item.UpdateQuantity(10);


        // Assert
        action.Should()
            .Throw<DomainException>()
            .WithMessage(
                "Cannot update a canceled item");
    }


    [Theory]
    [InlineData(1, 100, 100)]
    [InlineData(5, 100, 450)]
    [InlineData(10, 100, 800)]
    public void Constructor_Should_Calculate_Total_Correctly(
        int quantity,
        decimal unitPrice,
        decimal expectedTotal)
    {
        // Arrange & Act
        var item = new SaleItem(
            Guid.NewGuid(),
            quantity,
            unitPrice);


        // Assert
        item.Total.Should()
            .Be(expectedTotal);
    }
}