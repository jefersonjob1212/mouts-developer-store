using Ambev.DeveloperEvaluation.Domain.Interfaces;

namespace Ambev.DeveloperEvaluation.ORM;

public class UnitOfWork : IUnitOfWork
{
    private readonly DefaultContext _context;

    public UnitOfWork(DefaultContext context)
    {
        _context = context;
    }
    
    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}