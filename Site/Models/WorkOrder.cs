using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Site.Models
{
    public class WorkOrder
    {
        public Guid Id { get; set; }
        public DateTime? Date { get; set; }
        public String Customer { get; set; }
        public Guid? CrewId { get; set; }
        public String Description { get; set; }
        public Guid CategoryId { get; set; }
        public bool IsComplete { get; set; }

        public WorkOrderCategory Category { get; set; }
        public Crew Crew { get; set; }
        public ICollection<WorkOrderLog> WorkOrderLogs { get; set; }

        public string AssignedTo
        { 
            get 
            { 
                return Crew == null ? "Unassigned" : Crew.Name; 
            } 
        }

        public string DateDisplay
        {
            get
            {
                return Date.HasValue ?
                    Date.Value.ToString("dd-MMM-yyyy hh:mm") :
                    string.Empty;

            }
        }
    }
}