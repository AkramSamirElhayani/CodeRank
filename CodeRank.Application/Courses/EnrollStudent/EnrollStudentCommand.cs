using CodeRank.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeRank.Application.Courses.EnrollStudent;





public sealed record EnrollStudentCommand(
    Guid StudentId,
    Guid CourseId) : ICommand<Guid>;
