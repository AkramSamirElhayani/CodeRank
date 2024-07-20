namespace CodeRank.Domain;
using System;
using System.Collections.Generic;
using CodeRank.Domain.Abstractions;

public class Question : IEntity
{
    public Guid Id { get; private set; }
    public string Text { get; private set; } 
    public Guid QuizId { get; private set; }
    private List<QuestionTestCase> _testCases = new List<QuestionTestCase>();
    public IReadOnlyCollection<QuestionTestCase> TestCases => _testCases.AsReadOnly();

 
    private Question(Guid id, string text, Guid quizId)
    {
        Id = id;
        Text = text;
        QuizId = quizId;
    }

    public static Result<Question> Create(string text , Guid quizId)
    {
        var result = ValidateText(text);
        if (result.IsFailure) return Result.Failure<Question>(result.Errors);

        return Result.Success(new Question(Guid.NewGuid(), text,quizId));
    }

    public Result AddTestCase(QuestionTestCase testCase)
    {
        if (testCase == null)
            return Result.Failure(Error.CreateFormCallerName("Question", "Test case cannot be null."));

        if (_testCases.Contains(testCase))
            return Result.Failure(Error.CreateFormCallerName("Question", "This test case is already added to the question."));

        _testCases.Add(testCase);
        return Result.Success();
    }

    public Result RemoveTestCase(QuestionTestCase testCase)
    {
        if (!_testCases.Remove(testCase))
            return Result.Failure(Error.CreateFormCallerName("Question", "This test case is not part of the question."));

        return Result.Success();
    }

    private static Result ValidateText(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return Result.Failure(Error.CreateFormCallerName("Question", "Question text cannot be empty or whitespace."));
        return Result.Success();
    }
}
