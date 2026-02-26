using System;
using System.Collections.Generic;
using System.Text;

namespace Inspecta.Shared.Entities
{
    public class Checklist
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ChecklistItem> Items { get; set; } = new();
    }
}
