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
                if (!context.WorkOrders.Any(w => w.Date >= DateTime.Today)) {
                    context.Reset();
                }

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
                    .Where(w => !w.IsComplete && w.CrewId == crewId.Value)
                    .ToList();

                var viewModel = new CrewListViewModel(crew, workOrders);

                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult Index(WorkOrderLog input, Guid crewId, bool isComplete)
        {
            using (var context = new WorkOrderContext()) {

                var workOrder = context.WorkOrders.Include(w => w.WorkOrderLogs).First(w => w.Id == input.WorkOrderId);
                workOrder.IsComplete = isComplete;

                if (!workOrder.WorkOrderLogs.Any()) {
                    input.Date = DateTime.Now;
                    workOrder.WorkOrderLogs.Add(input);
                } else {
                    var log = workOrder.WorkOrderLogs.First();
                    log.Date = DateTime.Now;
                    log.Notes = input.Notes;
                }

                context.SaveChanges();

                return RedirectToAction("Index", new { crewId = crewId });
            }
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            using (var context = new WorkOrderContext()) {
                var workOrder = context.WorkOrders.Include(w => w.WorkOrderLogs).FirstOrDefault(w => w.Id == id);

                var log = workOrder.WorkOrderLogs.FirstOrDefault() ?? new WorkOrderLog();

                var viewModel = new EditWorkItemViewModel {
                    Id = log.Id,
                    WorkOrderId = workOrder.Id,
                    Title = "Enter work for work order",
                    Date = workOrder.DateDisplay,
                    Customer = workOrder.Customer,
                    Description = workOrder.Description,
                    Duration = workOrder.Duration,
                    Notes = log.Notes,
                    CrewId = workOrder.CrewId.Value,
                    IsComplete = workOrder.IsComplete
                };

                return View(viewModel);

            }
        }
    }
}
