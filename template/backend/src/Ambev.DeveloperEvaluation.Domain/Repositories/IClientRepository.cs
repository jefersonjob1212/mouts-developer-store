using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for Client entity operations
/// </summary>
public interface IClientRepository : IRepositoryBase<Client>
{
    /// <summary>
    /// Retrieves an individual clients with name
    /// </summary>
    /// <param name="name"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<IndividualClient>> GetByNameAsync(string name, CancellationToken cancellationToken);
    
    /// <summary>
    /// Retrieves an individual client with CPF
    /// </summary>
    /// <param name="cpf"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IndividualClient?> GetByCpfAsync(string cpf, CancellationToken cancellationToken);
    
    /// <summary>
    /// Retrieves a client with e-mail
    /// </summary>
    /// <param name="email"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Client?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    
    /// <summary>
    /// Retrieves a company client with CNPJ
    /// </summary>
    /// <param name="cnpj"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<CompanyClient?> GetByCnpjAsync(string cnpj, CancellationToken cancellationToken);
    
    /// <summary>
    /// Retrieves a company clients with legal name
    /// </summary>
    /// <param name="legalName"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<CompanyClient>> GetByLegalNameAsync(string legalName, CancellationToken cancellationToken);
    
    /// <summary>
    /// Retrieves a company clients with trade name
    /// </summary>
    /// <param name="tradeName"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<CompanyClient>> GetByTradeNameAsync(string tradeName, CancellationToken cancellationToken);
    
    /// <summary>
    /// Retrieves a client by term
    /// </summary>
    /// <param name="term"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IList<Client>> SearchByTermAsync(string term, CancellationToken cancellationToken);
}