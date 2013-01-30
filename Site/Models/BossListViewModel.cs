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
            Days = new List<BossListDayModel>();
            Dates = new List<string>();

            var firstDate = DateTime.Today;
            while (firstDate.DayOfWeek != DayOfWeek.Monday) {
                firstDate = firstDate.AddDays(-1);
            }

            var distinctCrews = workOrders.Select(w => w.Crew).Distinct().OrderBy(c => c.Name).ToList();
            CrewNames = distinctCrews.Select(c => c.Name).ToList();
            
            for (var i = 0; i < 5; i++) {
                var date = firstDate.AddDays(i);
                Dates.Add(date.ToString("dd-MMM-yyyy"));

                Days.Add(new BossListDayModel(date, distinctCrews, workOrders));
            }               
        }

        public string Title { get { return "Work Order List (All)"; } }
        public ICollection<string> CrewNames { get; set; }
        public ICollection<BossListDayModel> Days { get; set; }
        public ICollection<string> Dates { get; set; }
    }
}