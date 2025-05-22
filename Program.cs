using FirstAPI.Controllers;
using FirstAPI.Exceptions;
using FirstAPI.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;

var builder = WebApplication.CreateBuilder(args);

// Configure JSON serialization to handle enums as strings
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Configure database context
builder.Services.AddDbContext<ProjectDBContext>(options =>
    options.UseSqlite("Data Source=projects.db"));

// Register services
builder.Services.AddScoped<IValidator<Project>, ProjectValidator>();
builder.Services.AddEndpointsApiExplorer();

// Configure Swagger with enum string support
builder.Services.AddSwaggerGen(c =>
{
    c.UseAllOfToExtendReferenceSchemas();
    c.SchemaFilter<EnumSchemaFilter>();
});

var app = builder.Build();

// Middleware pipeline
app.UseMiddleware<ErrorHandlingMiddleware>();

// Initialize database
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ProjectDBContext>();
    db.Database.EnsureCreated();
}

// Swagger configuration
app.UseSwagger();
app.UseSwaggerUI();

// Routes
app.MapProjectRoutes();

app.Run();