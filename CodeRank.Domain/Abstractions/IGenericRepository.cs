using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CodeRank.Domain.Abstractions;


public interface IGenericRepository<TEntity> : IGenericRepository<TEntity, Guid>
    where TEntity : IEntity
{

}
public interface IGenericRepository<TEntity, TEntityKey>
where TEntity : IEntity<TEntityKey>
where TEntityKey : struct, IEquatable<TEntityKey>
{

    Task<TEntity?> GetAsync(TEntityKey key, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(TEntityKey key, CancellationToken cancellationToken = default);
    void Insert(TEntity entity);
    void Update(TEntity entity);
}
