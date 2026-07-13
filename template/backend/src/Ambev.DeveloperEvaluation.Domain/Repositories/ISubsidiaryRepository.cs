using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for Subsidiary entity operations
/// </summary>
public interface ISubsidiaryRepository : IRepositoryBase<Subsidiary>
{
    /// <summary>
    /// Retrieves all Subsidiaries
    /// </summary>
    /// <param name="cnpj"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Subsidiary?> GetByCnpjAsync(string cnpj, CancellationToken cancellationToken);
    
    /// <summary>
    /// Retrieves a Subsidiary by code
    /// </summary>
    /// <param name="legalName"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<Subsidiary>> GetByLegalNameAsync(string legalName, CancellationToken cancellationToken);
    
    /// <summary>
    /// Retrieves a Subsidiary by name
    /// </summary>
    /// <param name="tradeName"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<Subsidiary>> GetByTradeNameAsync(string tradeName, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a subsidiaries by term
    /// </summary>
    /// <param name="term"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<Subsidiary>> SearchByTermAsync(string term, CancellationToken cancellationToken);
}