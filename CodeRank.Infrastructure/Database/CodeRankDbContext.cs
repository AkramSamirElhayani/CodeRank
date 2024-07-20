using CodeRank.Application.Abstractions;
using CodeRank.Domain;
using CodeRank.Domain.Actor;
using CodeRank.Domain.Courses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CodeRank.Infrastructure.Database;

public sealed class CodeRankDbContext : DbContext, IUnitOfWork
{
    internal DbSet<Instructor> Instructors { get; set; }
    internal DbSet<Student> Students { get; set; }
    internal DbSet<CourseTopic> CourseTopics { get; set; }
    internal DbSet<Course> Courses { get; set; }
    internal DbSet<CourseEnrolment> CourseEnrolments { get; set; }
    internal DbSet<Quiz> Quizzes { get; set; }
    internal DbSet<QuizSubmission> QuizSubmissions { get; set; }
    internal DbSet<QuestionTestCase> QuestionTestCase { get; set; }
    internal DbSet<Question> Questions { get; set; }
    internal DbSet<QuestionSubmission> QuestionSubmissions { get; set; }

    public CodeRankDbContext(DbContextOptions options) : base(options)
    {

     


    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Course>()
            .HasOne<CourseTopic>()
            .WithMany()
            .HasForeignKey(x=>x.TopicId);

        modelBuilder
            .Entity<Course>()
            .HasOne<Instructor>()
            .WithMany()
            .HasForeignKey(c=>c.InstructorId);

        modelBuilder
            .Entity<CourseEnrolment>()
            .HasOne<Course>()
            .WithMany(c=>c.Enrollments)
            .HasForeignKey(ce=>ce.CourseId);


        modelBuilder
            .Entity<CourseEnrolment>()
            .HasOne<Student>()
            .WithMany()
            .HasForeignKey(ce=>ce.StudentId);


        modelBuilder
            .Entity<Quiz>()
            .HasOne<Course>()
            .WithMany()
            .HasForeignKey(qz=>qz.CourseId);


        modelBuilder
            .Entity<Quiz>()
            .HasMany<QuizSubmission>()
            .WithOne()
            .HasForeignKey(qs=>qs.QuizId);



        modelBuilder
            .Entity<Quiz>()
            .HasMany(x=>x.Questions)
            .WithOne()
            .HasForeignKey(qs=>qs.QuizId);

        modelBuilder
            .Entity<Question>()
            .HasMany<QuestionSubmission>()
            .WithOne()
            .HasForeignKey(qs=>qs.QuestionId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder
        .Entity<Question>()
        .HasMany(c=>c.TestCases)
        .WithOne()
        .HasForeignKey(tc=>tc.QuestionId);



        modelBuilder
      .Entity<QuestionSubmission>()
      .HasOne<QuestionTestCase>()
      .WithMany()
      .HasForeignKey(qs=>qs.TestCaseId);
    }
}
