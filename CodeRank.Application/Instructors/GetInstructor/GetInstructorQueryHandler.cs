using CodeRank.Application.Abstractions;
using CodeRank.Application.Teachers.GetInstructor;
using CodeRank.Domain.Abstractions;
using CodeRank.Domain.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeRank.Application.Instructors.GetInstructor
{
    internal sealed class GetInstructorQueryHandler : IQueryHandler<GetInstructorQuery, InstructorResponse>
    {
        private readonly IInstructorRepository _repository;

        public GetInstructorQueryHandler(IInstructorRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<InstructorResponse>> Handle(GetInstructorQuery request, CancellationToken cancellationToken)
        {
            var instructor = await _repository.GetAsync(request.id, cancellationToken);
            //TODO: Replace with Domain Errors 
            if (instructor == null)
                return Result.Failure<InstructorResponse>(new Error("Instructor.NotFound", "The instructor with the identifier {categoryId} was not found"));
            else 
                return new InstructorResponse(instructor.Id,instructor.Name);
        }
    }
}
