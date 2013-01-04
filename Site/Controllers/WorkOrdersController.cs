using Site.Models;
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
                return View(workorders);
            }
        }

    }
}
