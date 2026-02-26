using System;
using System.Collections.Generic;
using System.Text;

namespace Inspecta.Shared.Entities
{
    public class ChecklistItem
    {

        public int Id { get; set; }
        public string Question { get; set; } = string.Empty;
        public int ChecklistId { get; set; }

    }
}
