using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryBapp;
using System.Security.Claims;
using Microsoft.Owin.Security.Cookies;

namespace WebApp.Controllers
{
	[Authorize]
    public class PersonalController : Controller
    {
        public ActionResult Index()
        {
			ViewData["Message"] = "Personal";

            return View ();
        }

		public ActionResult Miembro()
		{
			ViewData["Message"] = "Miembro";

			return View();
		}


		//REST
		[HttpGet]
		public JsonResult ObtenerEmpleados()
		{
			var dbHelper = new EmpleadosHelper(EmpleadosHelper.GetConnection());
			var items = dbHelper.ObtenerEmpleados(2);//todo: obtener correctamente el id
			return Json(items, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public JsonResult ObtenerEmpleado(int id)
		{
			var dbHelper = new EmpleadosHelper(EmpleadosHelper.GetConnection());
			var item = dbHelper.ObtenerEmpleado(id);
			return Json(item, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public ActionResult GuardarEmpleado(Empleado item)
		{
			var nombre = item.Nombre;
			item.EmpresaId = 2; //todo
			var dbHelper = new EmpleadosHelper(EmpleadosHelper.GetConnection());
			var message = dbHelper.GuardarEmpleado(item);

			return Json(item);
		}
    }
}
