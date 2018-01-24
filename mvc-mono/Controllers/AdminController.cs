using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Runtime.Serialization;

using System.Text;


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


		public ActionResult Save(IEnumerable<HttpPostedFileBase> files)
		{
			// The Name of the Upload component is "files"
			if (files != null)
			{
				foreach (var file in files)
				{
					// Some browsers send file names with full path. This needs to be stripped.
					var fileName = Path.GetFileName(file.FileName);
					var physicalPath = Path.Combine(Server.MapPath("~/Content/Imagenes"), fileName);

					// The files are not actually saved in this demo
					 file.SaveAs(physicalPath);
				}
			}

			// Return an empty string to signify success
			return Content("");
		}

		public ActionResult Remove(string[] fileNames)
		{
			// The parameter of the Remove action must be called "fileNames"

			if (fileNames != null)
			{
				foreach (var fullName in fileNames)
				{
					var fileName = Path.GetFileName(fullName);
					var physicalPath = Path.Combine(Server.MapPath("~/Content/Imagenes"), fileName);

					// TODO: Verify user permissions

					if (System.IO.File.Exists(physicalPath))
					{
						// The files are not actually removed in this demo
						 System.IO.File.Delete(physicalPath);
					}
				}
			}

			// Return an empty string to signify success
			return Content("");
		}
	}
}