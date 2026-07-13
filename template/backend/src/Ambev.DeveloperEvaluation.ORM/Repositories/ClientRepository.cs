using Ambev.DeveloperEvaluation.Domain;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of IClientRepository using Entity Framework Core
/// </summary>
public class ClientRepository : RepositoryBase<Client>, IClientRepository
{
    private IQueryable<IndividualClient> _individualClients => _context.Clients.OfType<IndividualClient>();
    private IQueryable<CompanyClient> _companyClients => _context.Clients.OfType<CompanyClient>();
    
    /// <summary>
    /// Initialize a new instance
    /// </summary>
    /// <param name="context"></param>
    public ClientRepository(DefaultContext context)
        :base(context)
    {
    }
    
    /// <summary>
    /// Retrieves an individual clients with name
    /// </summary>
    /// <param name="name"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<IndividualClient>> GetByNameAsync(string name, CancellationToken cancellationToken)
        => await _context.Clients
            .OfType<IndividualClient>()
            .Where(x => x.Name == name)
            .ToListAsync(cancellationToken);

    /// <summary>
    /// Retrieves an individual client with CPF
    /// </summary>
    /// <param name="cpf"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<IndividualClient?> GetByCpfAsync(string cpf, CancellationToken cancellationToken)
        => _context.Clients.OfType<IndividualClient>().FirstOrDefaultAsync(x => x.Cpf == cpf, cancellationToken);

    /// <summary>
    /// Retrieves a client with e-mail
    /// </summary>
    /// <param name="email"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<Client?> GetByEmailAsync(string email, CancellationToken cancellationToken)
        => Query().FirstOrDefaultAsync(x => x.Email == email, cancellationToken);

    /// <summary>
    /// Retrieves a company client with CNPJ
    /// </summary>
    /// <param name="cnpj"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<CompanyClient?> GetByCnpjAsync(string cnpj, CancellationToken cancellationToken)
        => _context.Clients.OfType<CompanyClient>().FirstOrDefaultAsync(x => x.Cnpj == cnpj);

    /// <summary>
    /// Retrieves a company clients with legal name
    /// </summary>
    /// <param name="legalName"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<CompanyClient>> GetByLegalNameAsync(string legalName, CancellationToken cancellationToken)
        => await _context.Clients
                .OfType<CompanyClient>()
                .Where(x => x.LegalName.ToUpper().Contains(legalName.ToUpper()))
                .ToListAsync(cancellationToken);

    /// <summary>
    /// Retrieves a company clients with trade name
    /// </summary>
    /// <param name="tradeName"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<CompanyClient>> GetByTradeNameAsync(string tradeName, CancellationToken cancellationToken)
        => await _context.Clients
                .OfType<CompanyClient>()
                .Where(x => x.TradeName.ToUpper().Contains(tradeName.ToUpper()))
                .ToListAsync(cancellationToken);

    /// <summary>
    /// Retrieves a client by term
    /// </summary>
    /// <param name="term"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IList<Client>> SearchByTermAsync(string term, CancellationToken cancellationToken)
    {
        var result = new List<Client>();

        result.AddRange(await _individualClients
            .Where(x => x.Name.ToUpper().Contains(term.ToUpper()) ||
                        x.Cpf.Contains(term))
            .ToListAsync(cancellationToken));

        result.AddRange(await _companyClients
            .Where(x =>
                x.Cnpj.Contains(term) ||
                x.TradeName.ToUpper().Contains(term.ToUpper()) ||
                x.LegalName.ToUpper().Contains(term.ToUpper()))
            .ToListAsync(cancellationToken));

        return result;
    }
}