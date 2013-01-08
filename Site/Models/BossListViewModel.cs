using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Site.Models
{
    public class BossListViewModel
    {
        public BossListViewModel(ICollection<WorkOrder> workOrders)
        {
            var crews = new Dictionary<Crew, ICollection<WorkOrder>>();
            foreach (var workOrder in workOrders) {
                if (!crews.ContainsKey(workOrder.Crew)) {
                    crews.Add(workOrder.Crew, new List<WorkOrder>());
                }

                crews[workOrder.Crew].Add(workOrder);
            }

            Crews = crews.Keys.OrderBy(c => c.Name).Select(crew => new BossListCrewModel(crews[crew])).ToList();
        }

        public string Title { get { return "Work Order List (All)"; } }
        public ICollection<BossListCrewModel> Crews { get; set; }
    }
}