using System;

namespace CodeRank.Application.Abstractions;

public interface ICurrentUserService
{
    Guid UserId { get; }
    bool IsInstructor { get; }
}