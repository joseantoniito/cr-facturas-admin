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
    public class ArchivoXmlController : Controller
    {
        public ActionResult Index()
        {
			return View ();
        }
		/*
		//REST
		[HttpGet]
		public JsonResult ObtenerAgendas()
		{
			var dbHelper = new CitasHelper(CitasHelper.GetConnection());
			var items = dbHelper.ObtenerAgendas(2);//todo: obtener correctamente el id
			return Json(items, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public JsonResult ObtenerAgenda(int id)
		{
			var dbHelper = new CitasHelper(CitasHelper.GetConnection());
			var item = dbHelper.ObtenerAgenda(id);
			return Json(item, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public ActionResult GuardarAgenda(Agenda item)
		{
			item.EmpresaId = 2; //todo
			var dbHelper = new CitasHelper(CitasHelper.GetConnection());
			var message = dbHelper.GuardarAgenda(item);

			return Json(item);
		}



		[HttpGet]
		public JsonResult ObtenerHistorialVisitas()
		{
			var dbHelper = new CitasHelper(CitasHelper.GetConnection());
			var items = dbHelper.ObtenerHistorialVisitas(2);//todo: obtener correctamente el id
			return Json(items, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public JsonResult ObtenerHistorialVisita(int id)
		{
			var dbHelper = new CitasHelper(CitasHelper.GetConnection());
			var item = dbHelper.ObtenerHistorialVisita(id);
			return Json(item, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public ActionResult GuardarHistorialVisita(HistorialVisita item)
		{
			item.EmpresaId = 2; //todo
			var dbHelper = new CitasHelper(CitasHelper.GetConnection());
			var message = dbHelper.GuardarHistorialVisita(item);

			return Json(item);
		}*/

    }
}
