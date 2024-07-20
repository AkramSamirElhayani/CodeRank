using CodeRank.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeRank.Application.Students.CreateTeacher;

public sealed record CreateStudentCommand(string name ,string email):ICommand<Guid>;

