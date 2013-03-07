using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Site.Models;
using System.Data.Entity;

namespace Site.Controllers
{
    public class WorkController : Controller
    {
        public ActionResult Index(Guid? crewId = null)
        {
            using (var context = new WorkOrderContext()) {
                if (!(crewId.HasValue)) {
                    var crews = context.Crews.OrderBy(c => c.Name).ToList();
                    var model = new ChooseCrewModel { Crews = crews };

                    return View("ChooseCrew", model);
                }

                var crew = context.Crews.FirstOrDefault(c => c.Id == crewId.Value);

                var workOrders = context.WorkOrders
                    .Include(w => w.Crew)
                    .Include(w => w.Category)
                    .Include(w => w.WorkOrderLogs)
                    .Where(w => w.CrewId == crewId.Value)
                    .ToList();

                var viewModel = new CrewListViewModel(crew, workOrders);

                return View(viewModel);
            }
        }

    }
}
