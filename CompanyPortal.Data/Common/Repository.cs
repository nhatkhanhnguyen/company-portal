﻿using CompanyPortal.Core.Common;
using CompanyPortal.Core.Providers;
using CompanyPortal.Data.Database;

using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;

namespace CompanyPortal.Data.Common;

public sealed class RepositoryBase<TEntity>(ApplicationDbContext context, IUserPrincipalsProvider principalsProvider)
    : IRepository<TEntity>
    where TEntity : EntityBase
{
    private readonly string? _currentUserId = principalsProvider.GetCurrentUserId();
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    public IQueryable<TEntity> GetAll()
    {
        return _dbSet.AsNoTracking();
    }

    public Task<List<TEntity>> GetAllListAsync(CancellationToken cancellationToken)
    {
        return GetAll().ToListAsync(cancellationToken);
    }

    public Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return GetAll().Where(predicate).ToListAsync(cancellationToken);
    }

    public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbSet.Where(predicate);
    }

    public async Task<TEntity?> GetAsync(int id, CancellationToken cancellationToken)
    {
        return await _dbSet.FindAsync([id], cancellationToken);
    }

    public Task<TEntity?> SingleAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return _dbSet.SingleOrDefaultAsync(predicate, cancellationToken);
    }

    public Task<TEntity?> FirstOrDefaultAsync(int id, CancellationToken cancellationToken)
    {
        return _dbSet.Where(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);
    }

    public Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return _dbSet.Where(predicate).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
    }

    public async Task InsertAsync(TEntity entity, CancellationToken cancellationToken)
    {
        UpdateEntityInfo(entity);
        await _dbSet.AddAsync(entity, cancellationToken);
    }

    public Task InsertAsync(ICollection<TEntity> entities, CancellationToken cancellationToken)
    {
        foreach (var entity in entities)
        {
            UpdateEntityInfo(entity);
        }

        return _dbSet.AddRangeAsync(entities, cancellationToken);
    }

    public void Update(TEntity entity)
    {
        UpdateEntityInfo(entity);
        _dbSet.Update(entity);
    }

    public void Undelete(Expression<Func<TEntity, bool>> predicate)
    {
        var entities = _dbSet.Where(predicate);
        foreach (var entity in entities)
        {
            entity.IsActive = true;
        }
    }

    public async Task DeleteByIdAsync(int id, bool forceDelete = false, CancellationToken cancellationToken = default)
    {
        var entity = await _dbSet.FindAsync([id], cancellationToken: cancellationToken);
        if (entity != null)
        {
            if (forceDelete)
            {
                _dbSet.Remove(entity);
            }
            else
            {
                UpdateEntityInfo(entity, true);
                Update(entity);
            }
        }
    }

    public void Delete(TEntity? entity, bool forceDelete = false)
    {
        if (entity != null)
        {
            if (forceDelete)
            {
                _dbSet.Remove(entity);
            }
            else
            {
                UpdateEntityInfo(entity, true);
                Update(entity);
            }
        }
    }

    public void Delete(Expression<Func<TEntity, bool>> predicate, bool forceDelete = false)
    {
        var listEntity = _dbSet.Where(predicate);
        foreach (var entity in listEntity)
        {
            if (forceDelete)
            {
                _dbSet.Remove(entity);
            }
            else
            {
                UpdateEntityInfo(entity, true);
                _dbSet.Update(entity);
            }
        }
    }

    private void UpdateEntityInfo(TEntity entity, bool deleted = false)
    {
        if (deleted)
        {
            entity.DateDeleted = DateTimeOffset.UtcNow;
            if (!string.IsNullOrEmpty(_currentUserId))
            {
                entity.DeletedBy = _currentUserId;
            }

            entity.IsActive = false;
        }
        else
        {
            if (entity.IsTransient())
            {
                if (!string.IsNullOrEmpty(_currentUserId))
                {
                    entity.CreatedBy = _currentUserId;
                }

                entity.DateCreated = DateTimeOffset.UtcNow;
            }
            else
            {
                if (!string.IsNullOrEmpty(_currentUserId))
                {
                    entity.ModidifedBy = _currentUserId;
                }

                entity.DateModified = DateTimeOffset.UtcNow;
            }
        }
    }
}