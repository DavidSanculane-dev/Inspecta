using Inspecta.Server.Data;
using Inspecta.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inspecta.Server.Endpoints
{
    public static class ChecklistEndpoints
    {

        public static void MapChecklistEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/checklists");

            group.MapGet("/", async (InspectaDbContext db) =>
                await db.Checklists.Include(c => c.Items).ToListAsync());

            group.MapGet("/{id}", async (int id, InspectaDbContext db) =>
                await db.Checklists.Include(c => c.Items)
                    .FirstOrDefaultAsync(c => c.Id == id)
                    is Checklist checklist
                    ? Results.Ok(checklist)
                    : Results.NotFound());

            group.MapPost("/", async (Checklist checklist, InspectaDbContext db) =>
            {
                db.Checklists.Add(checklist);
                await db.SaveChangesAsync();
                return Results.Created($"/api/checklists/{checklist.Id}", checklist);
            });

            group.MapPut("/{id}", async (int id, Checklist updated, InspectaDbContext db) =>
            {
                var checklist = await db.Checklists.FindAsync(id);
                if (checklist is null) return Results.NotFound();

                checklist.Name = updated.Name;
                await db.SaveChangesAsync();

                return Results.NoContent();
            });

            group.MapDelete("/{id}", async (int id, InspectaDbContext db) =>
            {
                var checklist = await db.Checklists.FindAsync(id);
                if (checklist is null) return Results.NotFound();

                db.Checklists.Remove(checklist);
                await db.SaveChangesAsync();

                return Results.NoContent();
            });
        }

    }
}
