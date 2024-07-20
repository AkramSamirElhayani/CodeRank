using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeRank.Domain.Abstractions;

public interface IEntity<Key> where Key : struct, IEquatable<Key>
{
    public Key Id { get; }
}

public interface IEntity : IEntity<Guid>
{

}
