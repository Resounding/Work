using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Site.Models
{
    public class BossListCrewModel
    {
        public BossListCrewModel(Crew crew, ICollection<WorkOrder> workOrders)
        {
            Crew = crew;

            WorkOrders = workOrders.Where(w => w.Crew == crew).Select(w => new BossListWorkOrderModel(w)).ToList();
        }

        public Crew Crew { get; set; }
        public ICollection<BossListWorkOrderModel> WorkOrders { get; set; }
    }
}