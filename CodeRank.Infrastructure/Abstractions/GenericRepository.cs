using CodeRank.Application.Abstractions;
using CodeRank.Domain.Abstractions;
using CodeRank.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeRank.Infrastructure.Abstractions;

public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity>
      where TEntity : class, IEntity 
{
    private readonly CodeRankDbContext _db;

    public GenericRepository(CodeRankDbContext db)
    {
        _db = db;
    }

    public Task<bool> ExistsAsync(Guid key, CancellationToken cancellationToken = default)
    {
        return _db.Set<TEntity>().AnyAsync(e=>e.Id == key, cancellationToken);
    }

    public Task<TEntity?> GetAsync(Guid key, CancellationToken cancellationToken = default)
    {
        return _db.Set<TEntity>().FindAsync( key, cancellationToken).AsTask();
    }

    public void Insert(TEntity entity)
    {
        _db.Set<TEntity>().Add(entity);
    }

    public void Update(TEntity entity)
    {
        _db.Set<TEntity>().Update(entity);
    }
}
