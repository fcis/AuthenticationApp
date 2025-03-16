namespace AuthAPI.Domain.Entities
{
    /// <summary>
    /// Base class for all entities in the domain model
    /// Provides common properties for (all entities)
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Unique identifier for the entity
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// When the entity was created
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// When the entity was last modified
        /// </summary>
        public DateTime? ModifiedAt { get; set; }
    }
}