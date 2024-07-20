using CodeRank.Domain.Courses;
using CodeRank.Infrastructure.Abstractions;
using CodeRank.Infrastructure.Database;

namespace CodeRank.Infrastructure.Courses
{
    internal class CourseTopicRepository : GenericRepository<CourseTopic>,ICourseTopicRepository
    {
        public CourseTopicRepository(CodeRankDbContext db) : base(db)
        {
        }
    }
}