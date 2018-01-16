using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

		public ActionResult Default()
		{
			ViewData["Message"] = "Dashboard";

			return View();
		}

        public ActionResult Dashboard()
        {
            ViewData["Message"] = "Dashboard";

            return View();
        }

	
		public ActionResult Empresas()
		{
			ViewData["Message"] = "Empresas";

			return View();
		}

		public ActionResult Empresa()
		{
			ViewData["Message"] = "Empresa";

			return View();
		}
	}
}