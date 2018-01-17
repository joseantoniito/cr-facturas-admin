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
	public class ClientesController : Controller
	{
		public ActionResult Index()
		{
			ViewData["Message"] = "Clientes";

			return View ();
		}

		public ActionResult Item()
		{
			ViewData["Message"] = "Item";

			return View();
		}

		/*
		//REST
		[HttpGet]
		public JsonResult ObtenerClientes()
		{
			var dbHelper = new ClientesHelper(ClientesHelper.GetConnection());
			var items = dbHelper.ObtenerClientes(2);//todo: obtener correctamente el id
			return Json(items, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public JsonResult ObtenerCliente(int id)
		{
			var dbHelper = new ClientesHelper(ClientesHelper.GetConnection());
			var item = dbHelper.ObtenerCliente(id);
			return Json(item, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public ActionResult GuardarCliente(Cliente item)
		{
			var nombre = item.Nombre;
			item.EmpresaId = 2; //todo
			var dbHelper = new ClientesHelper(ClientesHelper.GetConnection());
			var message = dbHelper.GuardarCliente(item);

			return Json(item);
		}*/
	}
}
