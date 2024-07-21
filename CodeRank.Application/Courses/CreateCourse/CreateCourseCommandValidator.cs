// CreateCourse/CreateCourseCommand.cs
using FluentValidation;
namespace CodeRank.Application.Courses.CreateCourse;

internal sealed class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
{
    public CreateCourseCommandValidator()
    {
        RuleFor(c => c.Title)
            .NotEmpty();
        RuleFor(c => c.StartDate)
            .NotEmpty();
    }
}