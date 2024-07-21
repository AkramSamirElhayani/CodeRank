using CodeRank.Application.Abstractions;
using CodeRank.Domain.Abstractions;
using CodeRank.Domain.Actor;
using CodeRank.Domain.Courses;
using CodeRank.Infrastructure.Clock;
using CodeRank.Infrastructure.Courses;
using CodeRank.Infrastructure.Database;
using CodeRank.Infrastructure.Instructors;
using CodeRank.Infrastructure.Services;
using CodeRank.Infrastructure.Students;
using CodeRank.Presentation.Courses;
using CodeRank.Presentation.Instructors;
using CodeRank.Presentation.Students;
using FluentValidation;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeRank.Infrastructure;

public static class Module
{

    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        InstructorEndpoint.MapEndpoints(app); 
        StudentEndpoint.MapEndpoints(app);
        CourseEndpoints.MapEndpoints(app); 
    }

    public static IServiceCollection AddModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Application.AssemblyReference.Assembly);
        });

        services.AddValidatorsFromAssembly(Application.AssemblyReference.Assembly, includeInternalTypes: true);

        services.AddInfrastructure(configuration);

        return services;
    }

    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string databaseConnectionString = configuration.GetConnectionString("Database")!;


      
      
        services.AddDbContext<CodeRankDbContext>(
            (serviceProvider, options) =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Database")!);

            });
         
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<CodeRankDbContext>());


        services.TryAddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();


        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<IInstructorRepository, InstructorRepository>();
    }
}
