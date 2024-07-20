using CodeRank.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CodeRank.Api.Extensions;

internal static class MigrationExtensions
{
    internal static void ApplyMigrations<TDbContext>(this IApplicationBuilder app)
             where TDbContext : DbContext
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        ApplyMigration<TDbContext>(scope);
    }

    private static void ApplyMigration<TDbContext>(IServiceScope scope)
        where TDbContext : DbContext
    {
        using TDbContext context = scope.ServiceProvider.GetRequiredService<TDbContext>();

        context.Database.Migrate();
    }
}
