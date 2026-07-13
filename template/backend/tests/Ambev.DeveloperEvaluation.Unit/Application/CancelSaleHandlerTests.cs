using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Factories;
using Ambev.DeveloperEvaluation.Domain.Interfaces;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using Bogus;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;
using ValidationException = FluentValidation.ValidationException;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class CancelSaleHandlerTests
{
    private readonly ILogger<CancelSaleHandler> _logger;
    private readonly ISaleRepository _saleRepository;
    private readonly ISaleItemFactory _saleItemFactory;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    private readonly CancelSaleHandler _handler;

    public CancelSaleHandlerTests()
    {
        _logger = Substitute.For<ILogger<CancelSaleHandler>>();
        _saleRepository = Substitute.For<ISaleRepository>();
        _saleItemFactory = Substitute.For<ISaleItemFactory>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _mapper = Substitute.For<IMapper>();

        _handler = new CancelSaleHandler(
            _logger,
            _saleRepository,
            _saleItemFactory,
            _unitOfWork,
            _mapper);
    }


    [Fact]
    public async Task Handle_Should_Cancel_Sale_When_Sale_Exists()
    {
        // Arrange
        var sale = CreateSaleFaker().Generate();

        var command = new CancelSaleCommand
        {
            Id = sale.Id
        };

        var expectedResult = new CancelSaleResult
        {
            Id = sale.Id
        };


        _saleRepository
            .GetByIdWithItemsAsync(
                command.Id,
                Arg.Any<CancellationToken>())
            .Returns(sale);


        _mapper
            .Map<CancelSaleResult>(sale)
            .Returns(expectedResult);



        // Act
        var result = await _handler.Handle(
            command,
            CancellationToken.None);



        // Assert
        result.Should()
            .NotBeNull();

        result.Id.Should()
            .Be(sale.Id);


        sale.Status.Should()
            .Be(SaleStatus.Canceled);


        sale.Items.Should()
            .AllSatisfy(item =>
            {
                item.Status.Should()
                    .Be(SaleItemStatus.Canceled);
            });


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
    public async Task Handle_Should_Throw_ValidationException_When_Command_Is_Invalid()
    {
        // Arrange
        var command = new CancelSaleCommand
        {
            Id = Guid.Empty
        };


        // Act
        Func<Task> action = async () =>
            await _handler.Handle(
                command,
                CancellationToken.None);



        // Assert
        await action.Should()
            .ThrowAsync<ValidationException>();


        await _saleRepository
            .DidNotReceive()
            .GetByIdWithItemsAsync(
                Arg.Any<Guid>(),
                Arg.Any<CancellationToken>());


        await _unitOfWork
            .DidNotReceive()
            .CommitAsync(
                Arg.Any<CancellationToken>());
    }


    [Fact]
    public async Task Handle_Should_Throw_KeyNotFoundException_When_Sale_Does_Not_Exist()
    {
        // Arrange
        var command = new CancelSaleCommand
        {
            Id = Guid.NewGuid()
        };

        _saleRepository
            .GetByIdWithItemsAsync(
                command.Id,
                Arg.Any<CancellationToken>())
            .Returns((Sale)null!);



        // Act
        Func<Task> action = async () =>
            await _handler.Handle(
                command,
                CancellationToken.None);



        // Assert
        await action.Should()
            .ThrowAsync<KeyNotFoundException>()
            .WithMessage("Sale not found");


        await _saleRepository
            .DidNotReceive()
            .UpdateAsync(
                Arg.Any<Sale>(),
                Arg.Any<CancellationToken>());
    }
    
    private static Faker<Sale> CreateSaleFaker()
    {
        return new Faker<Sale>()
            .RuleFor(
                x => x.Id,
                _ => Guid.NewGuid())

            .RuleFor(
                x => x.Status,
                _ => SaleStatus.Created)

            .RuleFor(
                x => x.Items,
                f => CreateSaleItemFaker()
                    .Generate(3));
    }
    
    private static Faker<SaleItem> CreateSaleItemFaker()
    {
        return new Faker<SaleItem>()
            .CustomInstantiator(f =>
                new SaleItem(
                    Guid.NewGuid(),
                    f.Random.Int(1, 5),
                    f.Random.Decimal(10, 500)
                ));
    }
}