using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of ISubsidiaryRepository using Entity Framework Core
/// </summary>
public class SubsidiaryRepository : RepositoryBase<Subsidiary>, ISubsidiaryRepository
{
    public SubsidiaryRepository(DefaultContext context) : base(context)
    {
    }
    
    /// <summary>
    /// Retrieves a subsidiary by CNPJ
    /// </summary>
    /// <param name="cnpj"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Subsidiary?> GetByCnpjAsync(string cnpj, CancellationToken cancellationToken)
        => await Query().FirstOrDefaultAsync(x => x.Cnpj == cnpj, cancellationToken);

    /// <summary>
    /// Retrieves a Subsidiary by code
    /// </summary>
    /// <param name="legalName"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<IEnumerable<Subsidiary>> GetByLegalNameAsync(string legalName, CancellationToken cancellationToken)
        => Task.FromResult(Query().Where(x => 
            x.LegalName.ToUpper().Contains(legalName.ToUpper()))
            .AsEnumerable());
    
    /// <summary>
    /// Retrieves a Subsidiary by name
    /// </summary>
    /// <param name="tradeName"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<IEnumerable<Subsidiary>> GetByTradeNameAsync(string tradeName, CancellationToken cancellationToken)
        => Task.FromResult(Query().Where(p => p.TradeName.ToUpper().Contains(tradeName.ToUpper())).AsEnumerable());

    /// <summary>
    /// Retrieves a subsidiaries by term
    /// </summary>
    /// <param name="term"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Subsidiary>> SearchByTermAsync(string term, CancellationToken cancellationToken)
    {
        return await Query()
            .Where(s =>
                s.TradeName.ToUpper().Contains(term.ToUpper()) ||
                s.Cnpj.Contains(term) ||
                s.LegalName.ToUpper().Contains(term.ToUpper()))
            .ToListAsync(cancellationToken);
    }
}