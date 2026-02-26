using System;
using System.Collections.Generic;
using System.Text;

namespace Inspecta.Shared.Entities
{
    public class Inspection
    {

        public int Id { get; set; }
        public int ChecklistId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public List<InspectionItemResponse> Responses { get; set; } = new();


    }
}
