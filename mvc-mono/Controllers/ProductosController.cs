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
    public class ProductosController : Controller
    {
		public ActionResult Index()
		{
			ViewData["Message"] = "Productos";

			return View ();
		}

		public ActionResult Item()
		{
			ViewData["Message"] = "Item";

			return View();
		}


		//REST
		[HttpGet]
		public JsonResult ObtenerProductos()
		{
			var dbHelper = new ProductosHelper(ProductosHelper.GetConnection());
			var items = dbHelper.ObtenerProductos(2);//todo: obtener correctamente el id
			return Json(items, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public JsonResult ObtenerProducto(int id)
		{
			var dbHelper = new ProductosHelper(ProductosHelper.GetConnection());
			var item = dbHelper.ObtenerProducto(id);
			return Json(item, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public ActionResult GuardarProducto(Producto item)
		{
			var nombre = item.Nombre;
			item.EmpresaId = 2; //todo
			var dbHelper = new ProductosHelper(ProductosHelper.GetConnection());
			var message = dbHelper.GuardarProducto(item);

			return Json(item);
		}
    }
}
