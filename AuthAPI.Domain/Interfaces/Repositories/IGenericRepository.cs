using System.Linq.Expressions;

namespace AuthAPI.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Generic repository interface that defines basic CRUD operations
    /// </summary>
    /// <typeparam name="T">The entity type this repository works with</typeparam>
    public interface IGenericRepository<T> where T : class
    {
        /// <summary>
        /// Retrieves an entity by its ID
        /// </summary>
        /// <param name="id">The entity ID</param>
        /// <returns>The entity if found, null otherwise</returns>
        Task<T> GetByIdAsync(string id);

        /// <summary>
        /// Retrieves all entities of type T
        /// </summary>
        /// <returns>A read-only list of all entities</returns>
        Task<IReadOnlyList<T>> GetAllAsync();

        /// <summary>
        /// Finds entities that match the specified predicate
        /// </summary>
        /// <param name="predicate">The condition to filter entities</param>
        /// <returns>A read-only list of matching entities</returns>
        Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Gets the first entity that matches the specified predicate, or null if none is found
        /// </summary>
        /// <param name="predicate">The condition to filter entities</param>
        /// <returns>The matching entity or null</returns>
        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Adds a new entity to the repository
        /// </summary>
        /// <param name="entity">The entity to add</param>
        Task AddAsync(T entity);

        /// <summary>
        /// Updates an existing entity
        /// </summary>
        /// <param name="entity">The entity to update</param>
        void Update(T entity);

        /// <summary>
        /// Deletes an entity from the repository
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        void Delete(T entity);

        /// <summary>
        /// Checks if any entity matches the given predicate
        /// </summary>
        /// <param name="predicate">The condition to check</param>
        /// <returns>True if any entity matches, false otherwise</returns>
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Gets a queryable collection of the entities for more complex querying
        /// </summary>
        /// <returns>An IQueryable of the entities</returns>
        IQueryable<T> AsQueryable();
    }
}