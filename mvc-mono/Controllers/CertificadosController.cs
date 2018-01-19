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
	public class CertificadosController : Controller
	{
		public ActionResult Index()
		{
			ViewData["Message"] = "Certificados";

			return View ();
		}

		public ActionResult Item()
		{
			ViewData["Message"] = "Item";

			return View();
		}


		//REST
		[HttpGet]
		public JsonResult ObtenerCertificados()
		{
			var dbHelper = new CertificadosHelper(CertificadosHelper.GetConnection());
			var items = dbHelper.ObtenerCertificados(2);//todo: obtener correctamente el id
			return Json(items, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public JsonResult ObtenerCertificado(int id)
		{
			var dbHelper = new CertificadosHelper(CertificadosHelper.GetConnection());
			var item = dbHelper.ObtenerCertificado(id);
			return Json(item, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public ActionResult GuardarCertificado(Certificado item)
		{
			item.EmpresaId = 2; //todo
			item.Registrado = DateTime.Now;
	
			var dbHelper = new CertificadosHelper(CertificadosHelper.GetConnection());
			var message = dbHelper.GuardarCertificado(item);

			return Json(item);
		}
	}
}
