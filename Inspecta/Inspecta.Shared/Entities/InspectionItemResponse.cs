using System;
using System.Collections.Generic;
using System.Text;

namespace Inspecta.Shared.Entities
{
    public class InspectionItemResponse
    {

        public int Id { get; set; }
        public int InspectionId { get; set; }
        public int ChecklistItemId { get; set; }

        public string Response { get; set; } = string.Empty; // OK / NOK / Observação
        public string? ImagePath { get; set; } // Evidência


    }
}
