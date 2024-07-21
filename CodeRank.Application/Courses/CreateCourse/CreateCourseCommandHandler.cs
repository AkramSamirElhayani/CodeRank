using CodeRank.Application.Abstractions;
using CodeRank.Domain.Abstractions;
using CodeRank.Domain.Courses;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeRank.Application.Courses.CreateCourse;

public sealed class CreateCourseCommandHandler : ICommandHandler<CreateCourseCommand, Guid>
{
    private readonly ICourseRepository _courseRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    public CreateCourseCommandHandler(
        ICourseRepository courseRepository,
        IUnitOfWork unitOfWork,
        ICurrentUserService currentUserService)
    {
        _courseRepository = courseRepository;
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task<Result<Guid>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var instructorId = _currentUserService.UserId;
        if (instructorId == Guid.Empty)
        {
            return Result.Failure<Guid>(Error.CreateFormCallerName("CreateCourse", "User is not authenticated or is not an instructor."));
        }

        var courseResult = Course.Create(request.Title, instructorId, request.StartDate);
        if (courseResult.IsFailure)
            return Result.Failure<Guid>(courseResult.Errors);

        _courseRepository.Insert(courseResult.Value);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return courseResult.Value.Id;
    }
}