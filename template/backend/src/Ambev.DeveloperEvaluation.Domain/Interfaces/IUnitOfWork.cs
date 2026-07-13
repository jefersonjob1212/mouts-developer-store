namespace Ambev.DeveloperEvaluation.Domain.Interfaces;

/// <summary>
/// Interface for implementing the Unit of Work pattern to manage persistence transactions and data rollbacks.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Execute the commit transactions data
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task CommitAsync(CancellationToken cancellationToken);
}