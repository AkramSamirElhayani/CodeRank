namespace CodeRank.Domain.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using CodeRank.Domain.Abstractions;

public class Student:IEntity
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
  
    private Student(string name, string email)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
    }

    public static Result<Student> Create(string name, string email)
    {
        var nameResult = ValidateName(name);
        var emailResult = ValidateEmail(email);

        if (nameResult.IsFailure) return Result.Failure<Student>(nameResult.Errors);
        if (emailResult.IsFailure) return Result.Failure<Student>(emailResult.Errors);

        return Result.Success(new Student(name, email));
    }

    public Result SetName(string name)
    {
        var result = ValidateName(name);
        if (result.IsFailure) return result;

        Name = name;
        return Result.Success();
    }

    public Result SetEmail(string email)
    {
        var result = ValidateEmail(email);
        if (result.IsFailure) return result;

        Email = email;
        return Result.Success();
    }

    public Result Update(string name, string email)
    {
        var nameResult = ValidateName(name);
        var emailResult = ValidateEmail(email);

        var errors = new List<Error>();
        if (nameResult.IsFailure) errors.AddRange(nameResult.Errors);
        if (emailResult.IsFailure) errors.AddRange(emailResult.Errors);

        if (errors.Any())
            return Result.Failure(errors.ToArray());

        Name = name;
        Email = email;

        return Result.Success();
    }

    private static Result ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure(Error.CreateFormCallerName("Student", "Name cannot be empty or whitespace."));
        return Result.Success();
    }

    private static Result ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            return Result.Failure(Error.CreateFormCallerName("Student", "Invalid email address."));
        return Result.Success();
    }
}
