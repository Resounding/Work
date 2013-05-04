using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Site.Models
{
    public class CrewListViewModel
    {
        public CrewListViewModel(Crew crew, ICollection<WorkOrder> workOrders)
        {
            Crew = crew;

            Dates = new List<string>();
            Days = new List<CrewListDayModel>();

            var firstDate = DateTime.Today;
            while (firstDate.DayOfWeek == DayOfWeek.Saturday || firstDate.DayOfWeek == DayOfWeek.Sunday) {
                firstDate = firstDate.AddDays(1);
            }

            while (firstDate.DayOfWeek != DayOfWeek.Monday) {
                firstDate = firstDate.AddDays(-1);
            }

            for (var i = 0; i < 5; i++) {
                var date = firstDate.AddDays(i);
                Dates.Add(date.ToString("dd-MMM-yyyy"));

                Days.Add(new CrewListDayModel(date, workOrders));
            }
        }

        public string Title { get { return string.Format("Work Order List ({0})", Crew.Name); } }
        public Crew Crew { get; set; }
        public ICollection<string> Dates { get; set; }
        public ICollection<CrewListDayModel> Days { get; set; }
    }
}