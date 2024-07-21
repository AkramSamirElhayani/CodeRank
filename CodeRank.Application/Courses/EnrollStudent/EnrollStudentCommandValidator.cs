using FluentValidation;

namespace CodeRank.Application.Courses.EnrollStudent;

internal sealed class EnrollStudentCommandValidator : AbstractValidator<EnrollStudentCommand>
{
    public EnrollStudentCommandValidator()
    {
        RuleFor(c => c.StudentId)
            .NotEmpty();
        RuleFor(c => c.CourseId)
            .NotEmpty();
    }
}