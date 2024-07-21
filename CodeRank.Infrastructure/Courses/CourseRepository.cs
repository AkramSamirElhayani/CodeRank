using CodeRank.Domain.Courses;
using CodeRank.Infrastructure.Abstractions;
using CodeRank.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CodeRank.Infrastructure.Courses
{
    internal class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        public CourseRepository(CodeRankDbContext db) : base(db)
        {
        }

        public async Task<IEnumerable<Course>> GetCoursesByInstructorIdAsync(Guid instructorId, CancellationToken cancellationToken = default)
        {
           return await _db.Set<Course>().Where(c=>c.InstructorId == instructorId)
                .ToListAsync();
        }
    }
}