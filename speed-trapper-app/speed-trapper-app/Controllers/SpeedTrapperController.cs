using speed_trapper_app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace speed_trapper_app.Controllers
{
    public class SpeedTrapperController : Controller
    {
        //
        // GET: /SpeedTrapper/
        public ActionResult Index()
        {
            SpeedTrapperModel model = new SpeedTrapperModel();
            return View(model);
        }
	}
}