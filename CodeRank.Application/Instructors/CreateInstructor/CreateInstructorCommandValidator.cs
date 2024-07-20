using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeRank.Application.Instructors.CreateTeacher;

internal sealed class CreateInstructorCommandValidator : AbstractValidator<CreateInstructorCommand>
{
    public CreateInstructorCommandValidator()
    {
        RuleFor(t=>t.name)
            .NotEmpty();
        RuleFor(t=>t.email)
            .EmailAddress()
            .NotEmpty();
    }
}
