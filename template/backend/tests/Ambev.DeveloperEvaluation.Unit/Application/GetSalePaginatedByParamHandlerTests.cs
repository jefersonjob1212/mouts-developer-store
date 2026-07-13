using Ambev.DeveloperEvaluation.Application.Sales.GetSalePaginatedByParam;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Filters;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using Bogus;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class GetSalePaginatedByParamHandlerTests
{
    private readonly ISaleRepository _repository;
    private readonly IMapper _mapper;

    private readonly GetSalePaginatedByParamHandler _handler;

    public GetSalePaginatedByParamHandlerTests()
    {
        _repository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();

        _handler = new GetSalePaginatedByParamHandler(
            _repository,
            _mapper);
    }


    [Fact]
    public async Task Handle_Should_Return_Paginated_Sales_When_Sales_Exist()
    {
        // Arrange
        var command = new GetSalePaginatedByParamCommand(
            number: null,
            clientId: null,
            subsidiaryId: null,
            pageIndex: 1,
            pageSize: 5);
        
        var filter = new SaleFilter(
            new Random().NextInt64(), 
            Guid.NewGuid(), 
            Guid.NewGuid(), 
            1, 5);

        var sales = CreateSaleFaker()
            .Generate(5);

        var mappedResult = CreateSaleResultFaker()
            .Generate(5);

        _mapper
            .Map<GetSalePaginatedByParamCommand, SaleFilter>(command)
            .Returns(filter);
        
        _repository
            .GetPaginatedAsync(
                filter,
                Arg.Any<CancellationToken>())
            .Returns(sales);
        
        _repository
            .CountAsync(
                filter,
                Arg.Any<CancellationToken>())
            .Returns(20);
        
        _mapper
            .Map<IEnumerable<GetSalePaginatedByParamResult>>(sales)
            .Returns(mappedResult);
        
        // Act
        var result = await _handler.Handle(
            command,
            CancellationToken.None);
        
        // Assert
        result.Should()
            .NotBeNull();

        result.TotalCount
            .Should()
            .Be(20);

        result.TotalPages
            .Should()
            .Be(4);

        result.SaleList
            .Should()
            .HaveCount(5);
        
        _mapper
            .Received(1)
            .Map<GetSalePaginatedByParamCommand, SaleFilter>(
                command);
        
        await _repository
            .Received(1)
            .GetPaginatedAsync(
                filter,
                Arg.Any<CancellationToken>());
        
        await _repository
            .Received(1)
            .CountAsync(
                filter,
                Arg.Any<CancellationToken>());
    }



    [Fact]
    public async Task Handle_Should_Return_Empty_Result_When_No_Sales_Found()
    {
        // Arrange
        var command = CreateCommandFaker()
            .Generate();
        
        var filter = new SaleFilter(
            new Random().NextInt64(), 
            Guid.NewGuid(), 
            Guid.NewGuid(), 
            1, 5);
        
        _mapper
            .Map<GetSalePaginatedByParamCommand, SaleFilter>(command)
            .Returns(filter);
        
        _repository
            .GetPaginatedAsync(
                filter,
                Arg.Any<CancellationToken>())
            .Returns([]);
        
        _repository
            .CountAsync(
                filter,
                Arg.Any<CancellationToken>())
            .Returns(0);

        _mapper
            .Map<IEnumerable<GetSalePaginatedByParamResult>>(
                Arg.Any<IEnumerable<Sale>>())
            .Returns([]);
        
        // Act
        var result = await _handler.Handle(
            command,
            CancellationToken.None);

        // Assert
        result.TotalCount
            .Should()
            .Be(0);

        result.TotalPages
            .Should()
            .Be(0);
        
        result.SaleList
            .Should()
            .BeEmpty();
    }
    
    [Theory]
    [InlineData(10, 100, 10)]
    [InlineData(20, 100, 5)]
    [InlineData(5, 25, 5)]
    public async Task Handle_Should_Calculate_TotalPages_Correctly(
        int pageSize,
        int totalCount,
        int expectedPages)
    {
        // Arrange
        var command = CreateCommandFaker()
            .RuleFor(
                x => x.PageSize,
                _ => pageSize)
            .Generate();
        
        var filter = new SaleFilter(
            new Random().NextInt64(), 
            Guid.NewGuid(), 
            Guid.NewGuid(), 
            1, pageSize);

        _mapper
            .Map<GetSalePaginatedByParamCommand, SaleFilter>(command)
            .Returns(filter);

        _repository
            .GetPaginatedAsync(
                filter,
                Arg.Any<CancellationToken>())
            .Returns([]);

        _repository
            .CountAsync(
                filter,
                Arg.Any<CancellationToken>())
            .Returns(totalCount);



        _mapper
            .Map<IEnumerable<GetSalePaginatedByParamResult>>(
                Arg.Any<IEnumerable<Sale>>())
            .Returns([]);

        // Act
        var result = await _handler.Handle(
            command,
            CancellationToken.None);

        // Assert
        result.TotalPages
            .Should()
            .Be(expectedPages);
    }

    private static Faker<GetSalePaginatedByParamCommand> CreateCommandFaker()
    {
        return new Faker<GetSalePaginatedByParamCommand>()
            .CustomInstantiator(f =>
                new GetSalePaginatedByParamCommand(
                    f.Random.Long(1, 999999),
                    f.Random.Bool() ? Guid.NewGuid() : null,
                    f.Random.Bool() ? Guid.NewGuid() : null,
                    f.Random.Int(1, 10),
                    f.Random.Int(1, 50)));
    }

    private static Faker<Sale> CreateSaleFaker()
    {
        return new Faker<Sale>()
            .CustomInstantiator(_ =>
                new Sale(
                    new Random().Next(),
                    DateTime.UtcNow, 
                    Guid.NewGuid(),
                    Guid.NewGuid()));
    }
    
    private static Faker<GetSalePaginatedByParamResult> CreateSaleResultFaker()
    {
        return new Faker<GetSalePaginatedByParamResult>()
            .RuleFor(
                x => x.Id,
                _ => Guid.NewGuid());
    }
}