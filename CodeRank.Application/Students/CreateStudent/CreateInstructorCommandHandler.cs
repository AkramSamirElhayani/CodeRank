using CodeRank.Application.Abstractions;
using CodeRank.Domain.Abstractions;
using CodeRank.Domain.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeRank.Application.Students.CreateTeacher;

public sealed class CreateStudentCommandHandler 
    :ICommandHandler<CreateStudentCommand,Guid>
{
    private readonly IStudentRepository _StudentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateStudentCommandHandler(IStudentRepository StudentRepository, IUnitOfWork unitOfWork)
    {
        _StudentRepository = StudentRepository;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<Guid>> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        var StudentResult = Student.Create(request.name, request.email);
        if (StudentResult.IsFailure)
            return Result.Failure<Guid>(StudentResult.Errors);

        _StudentRepository.Insert(StudentResult.Value);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return StudentResult.Value.Id;
    }
}
