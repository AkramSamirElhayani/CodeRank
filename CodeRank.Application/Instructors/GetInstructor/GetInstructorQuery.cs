using CodeRank.Application.Abstractions;
using CodeRank.Application.Teachers.GetInstructor;
using CodeRank.Domain.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeRank.Application.Instructors.GetInstructor;

public sealed record GetInstructorQuery(Guid id):IQuery<InstructorResponse>;