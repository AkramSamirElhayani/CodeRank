using System.Text;
using System.Threading.Tasks;

namespace CodeRank.Domain;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using CodeRank.Domain.Abstractions;

public class QuestionSubmission : IEntity
{
    public Guid Id { get; private set; }
    public Guid QuestionId { get; private set; }
    public Guid TestCaseId { get; private set; }
    public string Input { get; private set; }
    public string ExpectedOutput { get; private set; }
    public string ActualOutput { get; private set; }
    public bool Passed { get; private set; }

    private QuestionSubmission(Guid questionId, Guid testCaseId, string input, string actualOutput, string expectedOutput)
    {
        Id = Guid.NewGuid();
        QuestionId = questionId;
        TestCaseId = testCaseId;
        Input = input;
        ActualOutput = actualOutput;
        Passed = actualOutput == ExpectedOutput;
        ExpectedOutput = expectedOutput;
    }

    internal static Result<QuestionSubmission> Create(Guid questionId, Guid testCaseId, string input, string actualOutput, string expectedOutput)
    {
        if (string.IsNullOrWhiteSpace(input))
            return Result.Failure<QuestionSubmission>(Error.CreateFormCallerName("QuestionSubmission", "Input cannot be empty or whitespace."));

        if (string.IsNullOrWhiteSpace(actualOutput))
            return Result.Failure<QuestionSubmission>(Error.CreateFormCallerName("QuestionSubmission", "Actual output cannot be empty or whitespace."));

        return Result.Success(new QuestionSubmission(questionId, testCaseId, input, actualOutput, expectedOutput));
    }
}
