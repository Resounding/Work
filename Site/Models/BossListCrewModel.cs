using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Site.Models
{
    public class BossListCrewModel
    {
        public BossListCrewModel(ICollection<WorkOrder> workOrders)
        {
            var days = new Dictionary<DateTime, ICollection<WorkOrder>>();

            foreach (var workOrder in workOrders) {
                var date = workOrder.Date.HasValue?workOrder.Date.Value:DateTime.MinValue;

                if (!days.ContainsKey(date)) {
                    days.Add(date, new List<WorkOrder>());
                }

                days[date].Add(workOrder);
            }

            Days = days.Keys.OrderBy(d => d).Select(day => new BossListDayModel(day, days[day])).ToList();
        }
        public ICollection<BossListDayModel> Days { get; set; }
    }
}