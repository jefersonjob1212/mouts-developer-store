using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
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

public class CreateSaleHandlerTests
{
    private readonly ILogger<CreateSaleHandler> _logger = Substitute.For<ILogger<CreateSaleHandler>>();
    private readonly ISaleRepository _saleRepository = Substitute.For<ISaleRepository>();
    private readonly ISaleItemFactory _saleItemFactory = Substitute.For<ISaleItemFactory>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly IMapper _mapper = Substitute.For<IMapper>();

    private readonly CreateSaleHandler _handler;

    public CreateSaleHandlerTests()
    {
        _handler = new CreateSaleHandler(
            _logger,
            _saleRepository,
            _saleItemFactory,
            _unitOfWork,
            _mapper);
    }
    
    [Fact]
    public async Task Handle_Should_Create_Sale_Successfully()
    {
        // Arrange

        var command = new CreateSaleHandlerTestData().Generate();

        var sale = new Sale(
            command.Number,
            DateTime.UtcNow,
            command.ClientId,
            command.SubsidiaryId);

        var saleItem = new SaleItem(
            command.Items.First().ProductId,
            100,
            command.Items.First().Quantity);

        var expected = new CreateSaleResult();

        _mapper
            .Map<Sale>(command)
            .Returns(sale);

        _saleItemFactory
            .CreateAsync(
                command.Items.First().ProductId,
                command.Items.First().Quantity,
                Arg.Any<CancellationToken>())
            .Returns(saleItem);

        _mapper
            .Map<CreateSaleResult>(sale)
            .Returns(expected);

        // Act

        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert

        result.Should().Be(expected);

        await _saleRepository
            .Received(1)
            .CreateAsync(sale, Arg.Any<CancellationToken>());

        await _unitOfWork
            .Received(1)
            .CommitAsync(Arg.Any<CancellationToken>());
    }
    
    [Fact]
    public async Task Handle_Should_Throw_When_Command_Is_Invalid()
    {
        // Arrange

        var command = new CreateSaleHandlerTestData()
            .RuleFor(x => x.Number, 0)
            .Generate();

        // Act

        Func<Task> action = async () =>
            await _handler.Handle(command, CancellationToken.None);

        // Assert

        await action.Should()
            .ThrowAsync<ValidationException>();

        await _saleRepository
            .DidNotReceive()
            .CreateAsync(
                Arg.Any<Sale>(),
                Arg.Any<CancellationToken>());

        await _unitOfWork
            .DidNotReceive()
            .CommitAsync(
                Arg.Any<CancellationToken>());
    }
}