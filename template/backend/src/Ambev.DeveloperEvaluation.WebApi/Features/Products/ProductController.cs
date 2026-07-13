using Ambev.DeveloperEvaluation.Application.Products.GetProductByCode;
using Ambev.DeveloperEvaluation.Application.Products.GetProductById;
using Ambev.DeveloperEvaluation.Application.Products.GetProductsByName;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products;

/// <summary>
/// Controllers for managing products operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ProductController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initialize a new instance of ProductController
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="mapper"></param>
    public ProductController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Retrieve a product with ID
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProductById(Guid id, CancellationToken cancellationToken)
    {
        var request = new GetProductByIdRequest { Id = id };
        var validator = new GetProductByIdRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);
        
        var query = _mapper.Map<GetProductByIdCommand>(request);
        var response = await _mediator.Send(query, cancellationToken);
        if(response is null)
            return NotFound();
        
        return Ok(_mapper.Map<GetProductResponse>(response));
    }

    /// <summary>
    /// Retrieve a product with code
    /// </summary>
    /// <param name="code"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("by-code/{code}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProductByCode(string code, CancellationToken cancellationToken)
    {
        var request = new GetProductByCodeRequest { Code = code };
        var validator = new GetProductByCodeRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);
        
        var query = _mapper.Map<GetProductByCodeCommand>(request);
        var response = await _mediator.Send(query, cancellationToken);
        if(response is null)
            return NotFound();
        
        return Ok(_mapper.Map<GetProductResponse>(response));
    }
    
    /// <summary>
    /// Retrieve a products with name
    /// </summary>
    /// <param name="name"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("by-name/{name}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProductsByName(string name, CancellationToken cancellationToken)
    {
        var request = new GetProductByNameRequest { Name = name };
        var validator = new GetProductByNameRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);
        
        var query = _mapper.Map<GetProductsByNameCommand>(request);
        var response = await _mediator.Send(query, cancellationToken);
        if(response is null)
            return NotFound();
        return Ok(_mapper.Map<IEnumerable<GetProductResponse>>(response));
    }
}