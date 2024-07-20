namespace CodeRank.Domain;
using System;
using System.Collections.Generic;
using CodeRank.Domain.Abstractions;

public class QuizSubmission : IEntity
{
    public Guid Id { get; private set; }
    public Guid StudentId { get; private set; }
    public Guid QuizId { get; private set; }
    public DateTime SubmissionDate { get; private set; }
    private List<QuestionSubmission> _questionSubmissions = new List<QuestionSubmission>();
    public IReadOnlyCollection<QuestionSubmission> QuestionSubmissions => _questionSubmissions.AsReadOnly();

    private QuizSubmission(Guid studentId, Guid quizId, DateTime submissionDate)
    {
        Id = Guid.NewGuid();
        StudentId = studentId;
        QuizId = quizId;
        SubmissionDate = submissionDate;
    }

    internal static Result<QuizSubmission> Create(Guid studentId, Guid quizId, DateTime submissionDate)
    {
        return Result.Success(new QuizSubmission(studentId, quizId, submissionDate));
    }




}
