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
    public class ItemsController : Controller
    {
        public ActionResult Index()
        {
			ViewData["Message"] = "Items";

            return View ();
        }

		public ActionResult Item()
		{
			ViewData["Message"] = "Item";

			return View();
		}


		//REST
		[HttpGet]
		public JsonResult ObtenerItems()
		{
			var dbHelper = new ItemsHelper(ItemsHelper.GetConnection());
			var items = dbHelper.ObtenerItems(2);//todo: obtener correctamente el id
			return Json(items, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public JsonResult ObtenerItem(int id)
		{
			var dbHelper = new ItemsHelper(ItemsHelper.GetConnection());
			var item = dbHelper.ObtenerItem(id);
			return Json(item, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public ActionResult GuardarItem(Item item)
		{
			item.EmpresaId = 2; //todo
			item.Activo = true;
			var dbHelper = new ItemsHelper(ItemsHelper.GetConnection());
			var message = dbHelper.GuardarItem(item);

			return Json(item);
		}
    }
}
