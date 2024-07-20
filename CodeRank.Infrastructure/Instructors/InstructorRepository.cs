using CodeRank.Domain.Actor;
using CodeRank.Domain.Courses;
using CodeRank.Infrastructure.Abstractions;
using CodeRank.Infrastructure.Database;

namespace CodeRank.Infrastructure.Instructors;

internal class InstructorRepository : GenericRepository<Instructor>,IInstructorRepository
{
    public InstructorRepository(CodeRankDbContext db) : base(db)
    {
    }
}