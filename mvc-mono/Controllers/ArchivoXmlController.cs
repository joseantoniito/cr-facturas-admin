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
			ViewData["Message"] = "Archivos";

			return View ();
		}

		public ActionResult Item()
		{
			ViewData["Message"] = "Archivo";

			return View();
		}


		//REST
		[HttpGet]
		public JsonResult ObtenerArchivos()
		{
			var dbHelper = new ArchivoXmlHelper(ArchivoXmlHelper.GetConnection());
			var items = dbHelper.ObtenerArchivos(2);//todo: obtener correctamente el id
			return Json(items, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public JsonResult ObtenerArchivo(int id)
		{
			var dbHelper = new ArchivoXmlHelper(ArchivoXmlHelper.GetConnection());
			var item = dbHelper.ObtenerArchivo(id);
			return Json(item, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public ActionResult GuardarArchivo(Archivo item)
		{
			//ArchivoId, EmpresaId, Ruta, StorageId, Registrado, EstadoArchivo, Hash, LlaveHacienda, 
			//TipoArchivo, UsuarioId, CertificadoId, ReceptorId
			item.EmpresaId = 2; //todo
			item.Registrado = DateTime.Now;
			item.EstadoArchivo = 1;
			item.UsuarioId = 1;//todo
			item.ReceptorId = 0;


			var dbHelper = new ArchivoXmlHelper(ArchivoXmlHelper.GetConnection());
			var message = dbHelper.GuardarArchivo(item);

			return Json(item);
		}
	}
}
