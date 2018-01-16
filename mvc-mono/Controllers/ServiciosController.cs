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
	public class ServiciosController : Controller
	{
		public ActionResult Index()
		{
			ViewData["Message"] = "Servicios";

			return View ();
		}

		public ActionResult Item()
		{
			ViewData["Message"] = "Item";

			return View();
		}


		//REST
		[HttpGet]
		public JsonResult ObtenerServicios()
		{
			var dbHelper = new ServiciosHelper(ServiciosHelper.GetConnection());
			var items = dbHelper.ObtenerServicios(2);//todo: obtener correctamente el id
			return Json(items, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public JsonResult ObtenerServicio(int id)
		{
			var dbHelper = new ServiciosHelper(ServiciosHelper.GetConnection());
			var item = dbHelper.ObtenerServicio(id);
			return Json(item, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public ActionResult GuardarServicio(Servicio item)
		{
			item.EmpresaId = 2; //todo
			var dbHelper = new ServiciosHelper(ServiciosHelper.GetConnection());
			var message = dbHelper.GuardarServicio(item);

			return Json(item);
		}
	}
}
