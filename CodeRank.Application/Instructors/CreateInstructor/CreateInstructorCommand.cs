using CodeRank.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeRank.Application.Instructors.CreateTeacher;

public sealed record CreateInstructorCommand(string name ,string email):ICommand<Guid>;

