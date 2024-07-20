using CodeRank.Domain.Courses;
using CodeRank.Infrastructure.Abstractions;
using CodeRank.Infrastructure.Database;

namespace CodeRank.Infrastructure.Courses
{
    internal class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        public CourseRepository(CodeRankDbContext db) : base(db)
        {
        }
    }
}