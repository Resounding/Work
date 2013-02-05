using Site.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Site.Controllers
{
    public class WorkOrdersController : Controller
    {
        public ActionResult Index()
        {
            using (var context = new WorkOrderContext()) {
                var workorders = context.WorkOrders.Include("Crew").Include("Category").Include("WorkOrderLogs").ToList();

                var viewModel = new BossListViewModel(workorders);
                return View(viewModel);
            }
        }

        public ActionResult New()
        {
            using (var context = new WorkOrderContext()) {
                var crews = context.Crews.OrderBy(c => c.Name).ToList();
                var categories = context.WorkOrderCategories.OrderBy(c => c.Name).ToList();
                var durations = new string[] { "", "1h", "2h", "4h", "1d", "2d", "3d", "4d", "5d" };
                var viewModel = new NewWorkOrderViewModel { 
                    Title = "New Work Order", 
                    Date = DateTime.Today.ToString("dd-MMM-yyyy"),
                    Crews = crews,
                    Durations = durations,
                    Categories = categories
                };
                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult Index(WorkOrder input)
        {
            using(var context = new WorkOrderContext()) {
                input.Id = Guid.NewGuid();
                context.WorkOrders.Add(input);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
        }
    }
}
