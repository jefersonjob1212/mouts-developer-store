using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Factories;
using Ambev.DeveloperEvaluation.Domain.Interfaces;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Domain;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class UpdateSaleHandlerTests
{
    private readonly ILogger<UpdateSaleHandler> _logger = Substitute.For<ILogger<UpdateSaleHandler>>();
    private readonly ISaleRepository _saleRepository = Substitute.For<ISaleRepository>();
    private readonly ISaleItemFactory _saleItemFactory = Substitute.For<ISaleItemFactory>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly UpdateSaleHandler _handler;

    public UpdateSaleHandlerTests()
    {
        _handler = new UpdateSaleHandler(
            _logger,
            _saleRepository,
            _saleItemFactory,
            _unitOfWork,
            _mapper);
    }
    
    [Fact]
    public async Task Handle_Should_Update_Existing_Sale()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var command = new UpdateSaleHandlerTestData()
            .RuleFor(x => x.Items,
                _ => new List<UpdateSaleItemCommand>
                {
                    new()
                    {
                        ProductId = productId,
                        Quantity = 10
                    }
                })
            .Generate();

        var sale = new Sale(
            command.Number,
            DateTime.UtcNow, 
            command.ClientId,
            command.SubsidiaryId);

        var existingItem = new SaleItem(
            productId,
            1,
            100);

        sale.AddItem(existingItem);

        var expected = new UpdateSaleResult();

        _saleRepository
            .GetByIdWithItemsAsync(command.Id, Arg.Any<CancellationToken>())
            .Returns(sale);

        _mapper
            .Map<UpdateSaleResult>(sale)
            .Returns(expected);

        sale.Items.Should().ContainSingle();

        sale.Items.First().ProductId.Should().Be(productId);

        sale.Items.First().Status.Should().Be(SaleItemStatus.Active);
        command.Items.Count.Should().Be(1);
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(expected);

        existingItem.Quantity.Should().Be(command.Items.First().Quantity);

        await _saleRepository.Received(1)
            .UpdateAsync(sale, Arg.Any<CancellationToken>());

        await _unitOfWork.Received(1)
            .CommitAsync(Arg.Any<CancellationToken>());

        await _saleItemFactory.DidNotReceive()
            .CreateAsync(
                Arg.Any<Guid>(),
                Arg.Any<int>(),
                Arg.Any<CancellationToken>());
    }
    
    [Fact]
    public async Task Handle_Should_Add_New_Item_When_Product_Does_Not_Exist()
    {
        // Arrange
        var productId = Guid.NewGuid();

        var command = new UpdateSaleCommand
        {
            Id = Guid.NewGuid(),
            Number = 1,
            ClientId = Guid.NewGuid(),
            SubsidiaryId = Guid.NewGuid(),
            Items =
            [
                new UpdateSaleItemCommand
                {
                    ProductId = productId,
                    Quantity = 20
                }
            ]
        };

        var sale = new Sale(
            command.Number,
            DateTime.UtcNow,
            command.ClientId,
            command.SubsidiaryId);


        var newItem = new SaleItem(
            productId,
            20,
            15);


        _saleRepository
            .GetByIdWithItemsAsync(
                command.Id,
                Arg.Any<CancellationToken>())
            .Returns(sale);


        _saleItemFactory
            .CreateAsync(
                Arg.Is<Guid>(x => x == productId),
                Arg.Is<int>(x => x == command.Items.First().Quantity),
                Arg.Any<CancellationToken>())
            .Returns(newItem);


        _mapper
            .Map<UpdateSaleResult>(sale)
            .Returns(new UpdateSaleResult());


        // Act
        await _handler.Handle(
            command,
            CancellationToken.None);


        // Assert
        sale.Items.Should()
            .ContainSingle();

        sale.Items.First()
            .Should()
            .Be(newItem);


        await _saleItemFactory
            .Received(1)
            .CreateAsync(
                productId,
                20,
                Arg.Any<CancellationToken>());


        await _saleRepository
            .Received(1)
            .UpdateAsync(
                sale,
                Arg.Any<CancellationToken>());


        await _unitOfWork
            .Received(1)
            .CommitAsync(
                Arg.Any<CancellationToken>());
    }
    
    [Fact]
    public async Task Handle_Should_Throw_When_Sale_Not_Found()
    {
        // Arrange

        var command = new UpdateSaleHandlerTestData().Generate();

        _saleRepository
            .GetByIdWithItemsAsync(command.Id, Arg.Any<CancellationToken>())
            .Returns((Sale?)null);

        // Act

        Func<Task> action = () =>
            _handler.Handle(command, CancellationToken.None);

        // Assert

        await action.Should()
            .ThrowAsync<KeyNotFoundException>()
            .WithMessage("Sale not found");

        await _saleRepository.DidNotReceive()
            .UpdateAsync(
                Arg.Any<Sale>(),
                Arg.Any<CancellationToken>());

        await _unitOfWork.DidNotReceive()
            .CommitAsync(
                Arg.Any<CancellationToken>());
    }
    
    [Fact]
    public async Task Handle_Should_Throw_When_Command_Is_Invalid()
    {
        // Arrange

        var command = new UpdateSaleHandlerTestData()
            .RuleFor(x => x.Number, 0)
            .Generate();

        // Act

        Func<Task> action = () =>
            _handler.Handle(command, CancellationToken.None);

        // Assert

        await action.Should()
            .ThrowAsync<ValidationException>();

        await _saleRepository.DidNotReceive()
            .GetByIdWithItemsAsync(
                Arg.Any<Guid>(),
                Arg.Any<CancellationToken>());
    }
}