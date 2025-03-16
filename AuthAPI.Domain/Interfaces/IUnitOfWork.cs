using AuthAPI.Domain.Interfaces.Repositories;

namespace AuthAPI.Domain.Interfaces
{
    /// <summary>
    /// Unit of Work pattern interface
    /// Manages transactions and provides access to repositories
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Gets the user repository
        /// </summary>
        IUserRepository Users { get; }

        /// <summary>
        /// Saves all changes made in this unit of work to the database
        /// </summary>
        /// <returns>The number of affected rows</returns>
        Task<int> SaveChangesAsync();

        /// <summary>
        /// Begins a new transaction
        /// </summary>
        Task BeginTransactionAsync();

        /// <summary>
        /// Commits the current transaction
        /// </summary>
        Task CommitTransactionAsync();

        /// <summary>
        /// Rolls back the current transaction
        /// </summary>
        Task RollbackTransactionAsync();
    }
}