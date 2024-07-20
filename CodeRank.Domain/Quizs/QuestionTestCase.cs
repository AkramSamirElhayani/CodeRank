namespace CodeRank.Domain;
using System;
using CodeRank.Domain.Abstractions;

public class QuestionTestCase
{
    public Guid Id { get; private set; }
    public Guid QuestionId { get; private set; }
    public string Name { get; private set; }
    public string Input { get; private set; }
    public string ExpectedOutput { get; private set; }

    private QuestionTestCase(string name, string input, string expectedOutput, Guid questionId)
    {
        Id = Guid.NewGuid();
        Name = name;
        Input = input;
        ExpectedOutput = expectedOutput;
        QuestionId = questionId;
    }

    public static Result<QuestionTestCase> Create(string name, string input, string expectedOutput, Guid questionId)
    {
        var nameResult = ValidateName(name);
        if (nameResult.IsFailure) return Result.Failure<QuestionTestCase>(nameResult.Errors);

        if (string.IsNullOrWhiteSpace(input))
            return Result.Failure<QuestionTestCase>(Error.CreateFormCallerName("QuestionTestCase", "Input cannot be empty or whitespace."));

        if (string.IsNullOrWhiteSpace(expectedOutput))
            return Result.Failure<QuestionTestCase>(Error.CreateFormCallerName("QuestionTestCase", "Expected output cannot be empty or whitespace."));

        return Result.Success(new QuestionTestCase(name, input, expectedOutput, questionId));
    }

    private static Result ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure(Error.CreateFormCallerName("QuestionTestCase", "Test case name cannot be empty or whitespace."));
        return Result.Success();
    }
}
