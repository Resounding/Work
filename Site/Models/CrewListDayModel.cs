using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Site.Models
{
    public class CrewListDayModel
    {
        public CrewListDayModel(DateTime date, ICollection<WorkOrder> workOrders)
        {
            WorkOrders = new List<CrewListWorkOrderModel>();

            foreach (var workOrder in workOrders) {
                if(workOrder.Date.HasValue && workOrder.Date.Value.Date==date.Date){
                    WorkOrders.Add(new CrewListWorkOrderModel(workOrder));
                }
            }
        }

        public ICollection<CrewListWorkOrderModel> WorkOrders { get; set; }
    }
}