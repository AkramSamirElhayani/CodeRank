using CodeRank.Application.Abstractions;
using CodeRank.Domain.Abstractions;
using CodeRank.Domain.Courses;

namespace CodeRank.Application.Courses.EnrollStudent;

public sealed class EnrollStudentCommandHandler : ICommandHandler<EnrollStudentCommand, Guid>
{
    private readonly ICourseRepository _courseRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EnrollStudentCommandHandler(ICourseRepository courseRepository, IUnitOfWork unitOfWork)
    {
        _courseRepository = courseRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(EnrollStudentCommand request, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetAsync(request.CourseId, cancellationToken);
        if (course == null)
            return Result.Failure<Guid>(Error.CreateFormCallerName("EnrollStudent", "Course not found"));

        var enrollmentResult = CourseEnrolment.Create(request.StudentId, request.CourseId, DateTime.UtcNow);
        if (enrollmentResult.IsFailure)
            return Result.Failure<Guid>(enrollmentResult.Errors);

        var result = course.EnrollStudent(enrollmentResult.Value);
        if (result.IsFailure)
            return Result.Failure<Guid>(result.Errors);

        _courseRepository.Update(course);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return enrollmentResult.Value.Id;
    }
}
