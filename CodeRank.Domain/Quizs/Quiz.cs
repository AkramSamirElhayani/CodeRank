namespace CodeRank.Domain;
using System;
using System.Collections.Generic;
using CodeRank.Domain.Abstractions;

public class Quiz : IEntity
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public Guid CourseId { get; private set; }

    private List<Question> _questions = new List<Question>();
    public IReadOnlyCollection<Question> Questions => _questions.AsReadOnly();
    public int AllowedSubmissions { get; private set; }

    private Quiz(string title, Guid courseId, int allowedSubmissions)
    {
        Id = Guid.NewGuid();
        Title = title;
        CourseId = courseId;
        AllowedSubmissions = allowedSubmissions;
    }

    public static Result<Quiz> Create(string title, Guid courseId, int allowedSubmissions)
    {
        var titleResult = ValidateTitle(title);
        if (titleResult.IsFailure) return Result.Failure<Quiz>(titleResult.Errors);
        if (allowedSubmissions < 0)
            return Result.Failure<Quiz>(Error.CreateFormCallerName("Quiz", "Allowed submissions cannot be negative."));

        return Result.Success(new Quiz(title, courseId, allowedSubmissions));
    }

    public Result AddQuestion(Question question)
    {
        if (question == null)
            return Result.Failure(Error.CreateFormCallerName("Quiz", "Question cannot be null."));

        if (_questions.Contains(question))
            return Result.Failure(Error.CreateFormCallerName("Quiz", "This question is already added to the quiz."));

        _questions.Add(question);
        return Result.Success();
    }

    public Result RemoveQuestion(Question question)
    {
        if (!_questions.Remove(question))
            return Result.Failure(Error.CreateFormCallerName("Quiz", "This question is not part of the quiz."));

        return Result.Success();
    }

    private static Result ValidateTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            return Result.Failure(Error.CreateFormCallerName("Quiz", "Quiz title cannot be empty or whitespace."));
        return Result.Success();
    }
}
