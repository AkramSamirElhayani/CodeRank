using CodeRank.Application.Abstractions;
using CodeRank.Domain.Abstractions;
using CodeRank.Domain.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeRank.Application.Instructors.CreateTeacher;

public sealed class CreateInstructorCommandHandler 
    :ICommandHandler<CreateInstructorCommand,Guid>
{
    private readonly IInstructorRepository _instructorRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateInstructorCommandHandler(IInstructorRepository instructorRepository, IUnitOfWork unitOfWork)
    {
        _instructorRepository = instructorRepository;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<Guid>> Handle(CreateInstructorCommand request, CancellationToken cancellationToken)
    {
        var instructorResult = Instructor.Create(request.name, request.email);
        if (instructorResult.IsFailure)
            return Result.Failure<Guid>(instructorResult.Errors);

        _instructorRepository.Insert(instructorResult.Value);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return instructorResult.Value.Id;
    }
}
