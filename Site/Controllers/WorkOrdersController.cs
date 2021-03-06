﻿using Site.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;

namespace Site.Controllers
{
    public class WorkOrdersController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            using (var context = new WorkOrderContext()) {

                if (!context.WorkOrders.Any(w => w.Date >= DateTime.Today)) {
                    context.Reset();
                }

                var workOrders = context.WorkOrders
                    .Include(w => w.Crew)
                    .Include(w => w.Category)
                    .Include(w => w.WorkOrderLogs)
                    .ToList();

                var viewModel = new BossListViewModel(workOrders);
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

                if (input.Duration.EndsWith("d", StringComparison.InvariantCultureIgnoreCase)) {
                    var duration = default(int);
                    if (int.TryParse(input.Duration.Replace("d", ""), out duration)) {
                        duration--;
                        var days = 1;
                        var date = input.Date.HasValue ? input.Date : (null as DateTime?);
                        while (days <= duration) {

                            if (date.HasValue) {
                                date = date.Value.AddDays(1);
                                if(date.Value.DayOfWeek == DayOfWeek.Saturday){
                                    date = date.Value.AddDays(2);
                                }

                                if (date.Value.DayOfWeek == DayOfWeek.Sunday) {
                                    date = date.Value.AddDays(1);
                                }
                            }

                            var nextWorkOrder = new WorkOrder {
                                Id = Guid.NewGuid(),
                                CategoryId=input.CategoryId,
                                CrewId=input.CrewId,
                                Customer=input.Customer,
                                Description=input.Description,
                                Duration=input.Duration,
                                Date = date
                            };
                            context.WorkOrders.Add(nextWorkOrder);

                            days++;
                        }
                    }
                }

                context.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult Edit(Guid? id = null)
        {
            if (!id.HasValue) return new HttpNotFoundResult();

            using (var context = new WorkOrderContext()) {
                var workOrder = context.WorkOrders.First(wo => wo.Id == id);

                var crews = context.Crews.OrderBy(c => c.Name).ToList();
                var categories = context.WorkOrderCategories.OrderBy(c => c.Name).ToList();
                var durations = new string[] { "", "1h", "2h", "4h", "1d", "2d", "3d", "4d", "5d" };

                var crewId = workOrder.CrewId ?? crews[0].Id;

                var viewModel = new EditWorkOrderViewModel {
                    Id = workOrder.Id,
                    Title = "Edit Work Order",
                    Date = workOrder.DateDisplay,
                    Customer = workOrder.Customer,
                    Description = workOrder.Description,
                    CrewId = crewId,
                    Crews = crews,
                    Duration = workOrder.Duration,
                    Durations = durations,
                    CategoryId = workOrder.CategoryId,
                    Categories = categories
                };

                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult Save(Guid? id, WorkOrder input)
        {
            using (var context = new WorkOrderContext()) {
                context.Entry(input).State = System.Data.EntityState.Modified;
                context.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Reset()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Reset(bool reset)
        {
            if (reset) {
                using (var context = new WorkOrderContext()) {
                    context.Database.SqlQuery<int>("DELETE dbo.WorkOrderLogs");
                    context.Database.SqlQuery<int>("DELETE dbo.WorkOrders");
                    context.Database.SqlQuery<int>("DELETE dbo.Crews");
                    context.Database.SqlQuery<int>("DELETE dbo.WorkOrderCategories");
                    context.Database.SqlQuery<int>("DELETE dbo.__MigrationHistory");
                    context.Database.Initialize(true);
                }
            }

            return RedirectToAction("Index");
        }
    }
}
