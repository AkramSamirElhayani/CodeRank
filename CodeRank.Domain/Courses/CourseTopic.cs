namespace CodeRank.Domain.Courses;
using System;
using CodeRank.Domain.Abstractions;

public class CourseTopic : IEntity
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }

    private CourseTopic(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }

    public static Result<CourseTopic> Create(string name)
    {
        var result = ValidateName(name);
        if (result.IsFailure) return Result.Failure<CourseTopic>(result.Errors);

        return Result.Success(new CourseTopic(name));
    }

    public Result SetName(string name)
    {
        var result = ValidateName(name);
        if (result.IsFailure) return result;

        Name = name;
        return Result.Success();
    }

    private static Result ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure(Error.CreateFormCallerName("CourseTopic", "Topic name cannot be empty or whitespace."));
        return Result.Success();
    }
}
