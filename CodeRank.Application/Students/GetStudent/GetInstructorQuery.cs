using CodeRank.Application.Abstractions;
using CodeRank.Application.Teachers.GetStudent;
using CodeRank.Domain.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeRank.Application.Students.GetStudent;

public sealed record GetStudentQuery(Guid id):IQuery<StudentResponse>;