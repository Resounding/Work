using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Site.Models
{
    public class WorkOrderLog
    {
        public Guid Id { get; set; }
        public Guid WorkOrderId { get; set; }
        public DateTime Date { get; set; }
        public String Notes { get; set; }

        public WorkOrder WorkOrder { get; set; }
        public Signature Signature { get; set; }
    }
}