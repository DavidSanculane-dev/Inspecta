using Inspecta.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inspecta.Server.Data
{
    public static class DbSeeder
    {

        public static async Task SeedAsync(InspectaDbContext db)
        {
            await db.Database.MigrateAsync();

            if (!await db.Checklists.AnyAsync())
            {
                var checklist = new Checklist
                {
                    Name = "Pre-Start Equipamento Pesado",
                    Items = new List<ChecklistItem>
                {
                    new() { Question = "Verificar níveis de óleo" },
                    new() { Question = "Verificar estado dos pneus" },
                    new() { Question = "Testar travões" }
                }
                };

                db.Checklists.Add(checklist);
                await db.SaveChangesAsync();
            }
        }


    }
}
