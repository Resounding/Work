using System;

namespace Site.Models
{
    public class EditWorkItemViewModel
    {
        public Guid Id { get; set; }
        public Guid WorkOrderId { get; set; }
        public Guid CrewId { get; set; }
        public string Title { get; set; }
        public string Date { get; set; }
        public String Customer { get; set; }
        public String Description { get; set; }
        public string Duration { get; set; }

        public string Notes { get; set; }
    }
}