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
    public class FacturacionController : Controller
    {
		public ActionResult Index()
		{
			ViewData["Message"] = "Facturación";

			return View ();
		}

		public ActionResult Item()
		{
			ViewData["Message"] = "Factura";

			return View();
		}


		//REST
		[HttpGet]
		public JsonResult ObtenerFacturaManuals()
		{
			var dbHelper = new FacturacionHelper(FacturacionHelper.GetConnection());
			var items = dbHelper.ObtenerFacturaManuals(2);//todo: obtener correctamente el id
			return Json(items, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public JsonResult ObtenerFacturaManual(int id)
		{
			var dbHelper = new FacturacionHelper(FacturacionHelper.GetConnection());
			var item = dbHelper.ObtenerFacturaManual(id);
			return Json(item, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public ActionResult GuardarFacturaManual(FacturaManual item)
		{
			item.EmpresaId = 2; //todo
			item.UsuarioId = 1;
			item.Registrado = DateTime.Now;
			var dbHelper = new FacturacionHelper(FacturacionHelper.GetConnection());
			var message = dbHelper.GuardarFacturaManual(item);

			return Json(item);
		}


		[HttpGet]
		public JsonResult ObtenerFacturaManualLineas()
		{
			var dbHelper = new FacturacionHelper(FacturacionHelper.GetConnection());
			var items = dbHelper.ObtenerFacturaManualLineas(2);//todo: obtener correctamente el id
			return Json(items, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public JsonResult ObtenerFacturaManualLinea(int id)
		{
			var dbHelper = new FacturacionHelper(FacturacionHelper.GetConnection());
			var item = dbHelper.ObtenerFacturaManualLinea(id);
			return Json(item, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public ActionResult GuardarFacturaManualLinea(FacturaManualLinea item)
		{
			//item.EmpresaId = 2; //todo
			var dbHelper = new FacturacionHelper(FacturacionHelper.GetConnection());
			var message = dbHelper.GuardarFacturaManualLinea(item);

			return Json(item);
		}
    }
}
