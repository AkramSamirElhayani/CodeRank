using CodeRank.Domain.Actor;
using CodeRank.Infrastructure.Abstractions;
using CodeRank.Infrastructure.Database;

namespace CodeRank.Infrastructure.Students;

internal class StudentRepository : GenericRepository<Student>,IStudentRepository
{
    public StudentRepository(CodeRankDbContext db) : base(db)
    {
    }
}