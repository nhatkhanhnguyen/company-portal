using CompanyPortal.Core.Common;

using System.Linq.Expressions;

namespace CompanyPortal.Data.Common;

public interface IRepository<TEntity> where TEntity : EntityBase
{
    #region Select/Get/Query

    /// <summary>
    ///     Used to get a IQueryable that is used to retrieve entities from entire table.
    /// </summary>
    /// <returns>IQueryable to be used to select entities from database</returns>
    IQueryable<TEntity> GetAll();

    /// <summary>
    ///     Used to get all entities.
    /// </summary>
    /// <returns>List of all entities</returns>
    Task<List<TEntity>> GetAllListAsync(CancellationToken cancellationToken);

    /// <summary>
    ///     Used to get all entities based on given <paramref name="predicate" />.
    /// </summary>
    /// <param name="predicate">A condition to filter entities</param>
    /// <param name="cancellationToken"></param>
    /// <returns>List of all entities</returns>
    Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

    /// <summary>
    ///     Queries the specified predicate.
    /// </summary>
    /// <param name="predicate">The predicate.</param>
    /// <returns></returns>
    IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    ///     Gets an entity with given primary key.
    /// </summary>
    /// <param name="id">Primary key of the entity to get</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Entity</returns>
    Task<TEntity?> GetAsync(int id, CancellationToken cancellationToken);

    /// <summary>
    ///     Gets exactly one entity with given predicate.
    ///     Throws exception if no entity or more than one entity.
    /// </summary>
    /// <param name="predicate">Entity</param>
    /// <param name="cancellationToken"></param>
    Task<TEntity?> SingleAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

    /// <summary>
    ///     Gets an entity with given primary key or null if not found.
    /// </summary>
    /// <param name="id">Primary key of the entity to get</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Entity or null</returns>
    Task<TEntity?> FirstOrDefaultAsync(int id, CancellationToken cancellationToken);

    /// <summary>
    ///     Gets an entity with given given predicate or null if not found.
    /// </summary>
    /// <param name="predicate">Predicate to filter entities</param>
    /// <param name="cancellationToken"></param>
    Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

    #endregion

    #region Insert

    /// <summary>
    ///     Inserts a new entity.
    /// </summary>
    /// <param name="entity">Inserted entity</param>
    /// <param name="cancellationToken"></param>
    Task InsertAsync(TEntity entity, CancellationToken cancellationToken);

    /// <summary>
    ///     Inserts a new entity.
    /// </summary>
    /// <param name="entities">Inserted entity</param>
    /// <param name="cancellationToken"></param>
    Task InsertAsync(ICollection<TEntity> entities, CancellationToken cancellationToken);

    #endregion

    #region Update

    /// <summary>
    ///     Updates an existing entity.
    /// </summary>
    /// <param name="entity">Entity</param>
    void Update(TEntity entity);
    
    #endregion

    #region Delete

    /// <summary>
    ///     Deletes an entity by primary key.
    /// </summary>
    /// <param name="id">Primary key of the entity</param>
    /// <param name="forceDelete">Completely remove entity if set to true, otherwise set IsActive = false</param>
    /// <param name="cancellationToken"></param>
    Task DeleteByIdAsync(int id, bool forceDelete = false,  CancellationToken cancellationToken = default);

    /// <summary>
    ///     Deletes many entities by function.
    ///     Notice that: All entities fits to given predicate are retrieved and deleted.
    ///     This may cause major performance problems if there are too many entities with
    ///     given predicate.
    /// </summary>
    /// <param name="predicate">A condition to filter entities</param>
    /// <param name="forceDelete">Completely remove entity if set to true, otherwise set IsActive = false</param>
    Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, bool forceDelete = false);

    #endregion

    #region Aggregates

    /// <summary>
    ///     Gets count of all entities in this repository.
    /// </summary>
    /// <returns>Count of entities</returns>
    Task<int> CountAsync(CancellationToken cancellationToken);

    /// <summary>
    ///     Gets count of all entities in this repository based on given <paramref name="predicate" />.
    /// </summary>
    /// <param name="predicate">A method to filter count</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Count of entities</returns>
    Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

    /// <summary>
    ///     Gets count of all entities in this repository (use if expected return value is greather than
    ///     <see cref="int.MaxValue" />.
    /// </summary>
    /// <returns>Count of entities</returns>
    Task<long> LongCountAsync(CancellationToken cancellationToken);

    /// <summary>
    ///     Gets count of all entities in this repository based on given <paramref name="predicate" />
    ///     (use this overload if expected return value is greather than <see cref="int.MaxValue" />).
    /// </summary>
    /// <param name="predicate">A method to filter count</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Count of entities</returns>
    Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

    #endregion
}
