using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FirstAPI.Controllers
{
    public static class ProjectController
    {
        public static void MapProjectRoutes(this WebApplication app)
        {
            app.MapGet("/projects", async (ProjectDBContext db,
            string? status,
            string? search,
            int pageNumber = 1,
            int pageSize = 10) =>
            {
                if(pageNumber < 1 || pageSize < 1)
                {
                    return Results.BadRequest("Page number and page size must be greater than 0.");
                }

                var querrry = db.Projects.AsQueryable();

                if(!string.IsNullOrEmpty(status) && Enum.TryParse(status, true, out Status parsedStatus))
                {
                    querrry = querrry.Where(p => p.Status == parsedStatus);
                }
                if(!string.IsNullOrEmpty(search))
                {
                    querrry = querrry.Where(p => p.Title.ToLower().Contains(search.ToLower()));
                }
                var totalCount = await querrry.CountAsync();
                
                var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                var projects = await querrry
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
                var response = new
                {
                    TotalCount = totalCount,
                    TotalPages = totalPages,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    Projects = projects
                };
                return Results.Ok(response);
            });

            app.MapGet("/projects/{id}", async (int id, ProjectDBContext db) =>
            {
                return await db.Projects.FindAsync(id)
                    is Project project
                        ? Results.Ok(project)
                        : Results.NotFound();
            });

            app.MapPost("/projects", async (Project project, ProjectDBContext db, IValidator<Project> validator) =>
            {
                using var transaction = await db.Database.BeginTransactionAsync();
                var validationResult = await validator.ValidateAsync(project);
                if (!validationResult.IsValid)
                {
                    return Results.ValidationProblem(validationResult.ToDictionary());
                }

                db.Projects.Add(project);
                await db.SaveChangesAsync();

                await transaction.CommitAsync();
                

                return Results.Created($"/projects/{project.Id}", project);
            });

            app.MapPut("/projects/{id}", async (int id, Project inputProject, ProjectDBContext db, IValidator<Project> validator) =>
            {
                var validationResult = await validator.ValidateAsync(inputProject);
                if (!validationResult.IsValid)
                {
                    return Results.BadRequest(validationResult.Errors);
                }

                var project = await db.Projects.FindAsync(id);
                if (project is null) return Results.NotFound();

                project.Title = inputProject.Title;
                project.Description = inputProject.Description;
                project.StartDate = inputProject.StartDate;
                project.Budget = inputProject.Budget;
                project.Status = inputProject.Status;
                project.Contributors = inputProject.Contributors;

                await db.SaveChangesAsync();

                return Results.NoContent();
            });

            app.MapDelete("/projects/{id}", async (int id, ProjectDBContext db) =>
            {
                using var transaction = await db.Database.BeginTransactionAsync();

                var project = await db.Projects.FindAsync(id);
                if (project is null) return Results.NotFound();

                db.Projects.Remove(project);
                await db.SaveChangesAsync();

                await transaction.CommitAsync();

                return Results.NoContent();
            });
        }
    }
}