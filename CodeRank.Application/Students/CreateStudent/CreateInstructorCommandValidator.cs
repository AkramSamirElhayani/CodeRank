using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeRank.Application.Students.CreateTeacher;

internal sealed class CreateStudentCommandValidator : AbstractValidator<CreateStudentCommand>
{
    public CreateStudentCommandValidator()
    {
        RuleFor(t=>t.name)
            .NotEmpty();
        RuleFor(t=>t.email)
            .EmailAddress()
            .NotEmpty();
    }
}
