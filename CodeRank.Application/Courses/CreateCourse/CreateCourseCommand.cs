using CodeRank.Application.Abstractions;
using System;

namespace CodeRank.Application.Courses.CreateCourse;

public sealed record CreateCourseCommand(
    string Title,
    DateTime StartDate) : ICommand<Guid>;