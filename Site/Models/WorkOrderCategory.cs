using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Site.Models
{
    public class WorkOrderCategory
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String CssClass { get; set; }

        public ICollection<WorkOrder> WorkOrders { get; set; }
    }
}