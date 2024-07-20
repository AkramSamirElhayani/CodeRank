using CodeRank.Application.Abstractions;
using CodeRank.Application.Teachers.GetStudent;
using CodeRank.Domain.Abstractions;
using CodeRank.Domain.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeRank.Application.Students.GetStudent
{
    internal sealed class GetStudentQueryHandler : IQueryHandler<GetStudentQuery, StudentResponse>
    {
        private readonly IStudentRepository _repository;

        public GetStudentQueryHandler(IStudentRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<StudentResponse>> Handle(GetStudentQuery request, CancellationToken cancellationToken)
        {
            var Student = await _repository.GetAsync(request.id, cancellationToken);
            //TODO: Replace with Domain Errors 
            if (Student == null)
                return Result.Failure<StudentResponse>(new Error("Student.NotFound", "The Student with the identifier {categoryId} was not found"));
            else 
                return new StudentResponse(Student.Id,Student.Name);
        }
    }
}
