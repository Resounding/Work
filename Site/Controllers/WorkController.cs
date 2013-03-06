using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Site.Models;

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

                return View();
            }
        }

    }
}
