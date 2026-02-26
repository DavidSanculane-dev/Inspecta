using Microsoft.EntityFrameworkCore;
using Inspecta.Shared.Entities;

namespace Inspecta.Server.Data
{
    public class InspectaDbContext : DbContext
    {

        public InspectaDbContext(DbContextOptions<InspectaDbContext> options)
                    : base(options) { }

        public DbSet<Checklist> Checklists { get; set; }
        public DbSet<ChecklistItem> ChecklistItems { get; set; }
        public DbSet<Inspection> Inspections { get; set; }
        public DbSet<InspectionItemResponse> InspectionItemResponses { get; set; }


    }
}
