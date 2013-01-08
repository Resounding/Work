using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Site.Models
{
    public class BossListDayModel
    {
        public BossListDayModel(DateTime date, ICollection<WorkOrder> workOrders)
        {
            Date = (date == DateTime.MinValue) ? "Unscheduled" : date.ToString("dd-MMM-yyyy");

            WorkOrders = workOrders.Select(w => new BossListWorkOrderModel(w)).OrderBy(w => w.Order).ToList();
        }

        public string Date { get; set; }
        public ICollection<BossListWorkOrderModel> WorkOrders { get; set; }
    }
}