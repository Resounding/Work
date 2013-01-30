using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Site.Models
{
    public class BossListDayModel
    {
        public BossListDayModel(DateTime date, ICollection<Crew> crews, ICollection<WorkOrder> workOrders)
        {
            Date = (date == DateTime.MinValue) ? "Unscheduled" : date.ToString("dd-MMM-yyyy");

            var validWorkOrders = new List<WorkOrder>();

            foreach (var workOrder in workOrders) {
                if (workOrder.Date.HasValue && workOrder.Date.Value == date && crews.Contains(workOrder.Crew)) {
                    validWorkOrders.Add(workOrder);
                }
            }

            Crews = crews.Select(c => new BossListCrewModel(c, validWorkOrders)).ToList();
        }

        public string Date { get; set; }
        public ICollection<BossListCrewModel> Crews { get; set; }
    }
}