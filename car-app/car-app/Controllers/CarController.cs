using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using car_app.Models;
namespace car_app.Controllers
{
    public class CarController : Controller
    {
        //
        // GET: /Car/
        public ActionResult Index()
        {
            CarModel model = CarModel.GetRandomCar();

            return View(model);
        }
	}
}