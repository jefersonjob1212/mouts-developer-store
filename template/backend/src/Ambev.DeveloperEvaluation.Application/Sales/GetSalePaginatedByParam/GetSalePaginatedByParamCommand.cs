using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSalePaginatedByParam;

/// <summary>
/// Command for retrieving a sales by params
/// </summary>
public class GetSalePaginatedByParamCommand : IRequest<GetSalePaginatedResult>
{
    /// <summary>
    /// The unique number of the sale to retrieve
    /// </summary>
    public long? Number { get; set; }

    /// <summary>
    /// The client ID of the sale to retrieve
    /// </summary>
    public Guid? ClientId { get; set; }

    /// <summary>
    /// The subsidiary ID of the sale to retrieve
    /// </summary>
    public Guid? SubsidiaryId { get; set; }
    
    /// <summary>
    /// The page for retrieve a list of sales
    /// </summary>
    public int PageIndex { get; set; }
    
    /// <summary>
    /// The size for limit retrieves a list of sales
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Initialize a new instance of GetSalePaginatedByParamCommand
    /// </summary>
    /// <param name="number"></param>
    /// <param name="clientId"></param>
    /// <param name="subsidiaryId"></param>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    public GetSalePaginatedByParamCommand(
        long? number, 
        Guid? clientId, 
        Guid? subsidiaryId, 
        int pageIndex, 
        int pageSize)
    {
        Number = number;
        ClientId = clientId;
        SubsidiaryId = subsidiaryId;
        PageIndex = pageIndex;
        PageSize = pageSize;
    }
}