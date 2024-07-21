using CodeRank.Api.Extensions;
using CodeRank.API.Identity;
using CodeRank.API.Identity.TokenHelpers;
using CodeRank.Application.Abstractions;
using CodeRank.Infrastructure;
using CodeRank.Infrastructure.Database;
using CodeRank.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationIdentityDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<ApplicationIdentityDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {

        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration.GetSection("JwtSettings")["JwtIssuer"],
        ValidAudience = builder.Configuration.GetSection("JwtSettings")["JwtAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JwtSettings")["JwtKey"]))
    };
});

builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("InstructorPolicy", policy =>
        policy.RequireRole("Instructor"));
    options.AddPolicy("InstructorPolicy", policy =>
       policy.RequireRole("Instructor"));
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddModule(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations<CodeRankDbContext>();
    app.ApplyMigrations<ApplicationIdentityDbContext>();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await IdentityExtensions.CreateRoles(services);
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthentication();
app.UseMiddleware<RBACMiddleware>();
app.MapControllers();
app.UseCors();

var apiGroup = app.MapGroup("/api")
.WithTags("API")
.RequireAuthorization();

Module.MapEndpoints(apiGroup);

app.Run();
