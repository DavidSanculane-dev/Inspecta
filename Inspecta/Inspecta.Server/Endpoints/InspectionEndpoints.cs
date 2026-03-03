
using Inspecta.Server.Data;
using Inspecta.Shared.Entities;
using Microsoft.EntityFrameworkCore;


namespace Inspecta.Server.Endpoints
{
    public static class InspectionEndpoints
    {

        public static void MapInspectionEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/inspections");

            // Lista todas as inspeções (simples)
            group.MapGet("/", async (InspectaDbContext db) =>
                await db.Inspections
                    .Include(i => i.Responses)
                    .ToListAsync());

            // Obter uma inspeção por id
            group.MapGet("/{id:int}", async (int id, InspectaDbContext db) =>
            {
                var insp = await db.Inspections
                    .Include(i => i.Responses)
                    .FirstOrDefaultAsync(i => i.Id == id);

                return insp is null ? Results.NotFound() : Results.Ok(insp);
            });

            // Criar uma inspeção a partir de um checklist
            group.MapPost("/", async (int checklistId, InspectaDbContext db) =>
            {
                // valida checklist
                var checklist = await db.Checklists.Include(c => c.Items).FirstOrDefaultAsync(c => c.Id == checklistId);
                if (checklist is null) return Results.BadRequest("Checklist não encontrado.");

                var inspection = new Inspection
                {
                    ChecklistId = checklist.Id,
                    CreatedAt = DateTime.UtcNow,
                    Responses = checklist.Items.Select(ci => new InspectionItemResponse
                    {
                        ChecklistItemId = ci.Id,
                        Response = "" // vazio inicialmente
                    }).ToList()
                };

                db.Inspections.Add(inspection);
                await db.SaveChangesAsync();

                return Results.Created($"/api/inspections/{inspection.Id}", inspection);
            });

            // Responder a um item da inspeção
            group.MapPut("/{inspectionId:int}/responses/{responseId:int}", async (
                int inspectionId, int responseId, InspectionItemResponse update, InspectaDbContext db) =>
            {
                var insp = await db.Inspections.Include(i => i.Responses)
                                               .FirstOrDefaultAsync(i => i.Id == inspectionId);
                if (insp is null) return Results.NotFound("Inspeção não encontrada.");

                var resp = insp.Responses.FirstOrDefault(r => r.Id == responseId);
                if (resp is null) return Results.NotFound("Resposta não encontrada.");

                resp.Response = update.Response;
                resp.ImagePath = update.ImagePath;
                await db.SaveChangesAsync();

                return Results.NoContent();
            });

            // Apagar inspeção
            group.MapDelete("/{id:int}", async (int id, InspectaDbContext db) =>
            {
                var insp = await db.Inspections.FindAsync(id);
                if (insp is null) return Results.NotFound();

                db.Inspections.Remove(insp);
                await db.SaveChangesAsync();
                return Results.NoContent();
            });
        }


    }
}
