using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryBapp;
using System.Security.Claims;
//using System.Security.Authentication;
using Microsoft.Owin.Security.Cookies;

namespace WebApp.Controllers
{
        //[Route("WebApp/[controller]")] //todo: quitar este parametro, no funciona
        public class UserController : Controller
        {
			[HttpGet]
			public JsonResult ObtenerEmpresas()
			{
				var dbHelper = new DataBaseHelper(DataBaseHelper.GetConnection());
				var items = dbHelper.ObtenerEmpresas();
				return Json(items, JsonRequestBehavior.AllowGet);
			}

			[HttpGet]
			public JsonResult ObtenerEmpresa(int id)
			{
				var dbHelper = new DataBaseHelper(DataBaseHelper.GetConnection());
				var item = dbHelper.ObtenerEmpresa(id);
				return Json(item, JsonRequestBehavior.AllowGet);
			}

			[HttpPost]
			public ActionResult GuardarEmpresa(Empresa item)
			{
				var nombre = item.Nombre;

				var dbHelper = new DataBaseHelper(DataBaseHelper.GetConnection());
				var message = dbHelper.GuardarEmpresa(item);

				return Json(item);
			}



			[HttpGet]
			public JsonResult ObtenerCategorias()
			{
				var dbHelper = new DataBaseHelper(DataBaseHelper.GetConnection());
				var items = dbHelper.ObtenerCategorias(2);//todo: insertar correctamente el id
				return Json(items, JsonRequestBehavior.AllowGet);
			}

			[HttpGet]
			public JsonResult ObtenerCategoria(int id)
			{
				var dbHelper = new DataBaseHelper(DataBaseHelper.GetConnection());
				var item = dbHelper.ObtenerCategoria(id);
				return Json(item, JsonRequestBehavior.AllowGet);
			}

			[HttpPost]
			public ActionResult GuardarCategoria(Categoria item)
			{
				var nombre = item.Nombre;
				item.EmpresaId = 2; //todo
				var dbHelper = new DataBaseHelper(DataBaseHelper.GetConnection());
				var message = dbHelper.GuardarCategoria(item);

				return Json(item);
			}


				
			

			/*FUNCIONES ANTERIORES  DE REFERENCIA
    

            [Authorize]
            [HttpGet]
            public JsonResult ObtenerRutas()
            {
                var id_u = HttpContext.GetOwinContext().Authentication.User.Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.UserData).Value;

                var dbHelper = new DataBaseHelper(DataBaseHelper.GetConnection());
                var rutas = dbHelper.ObtenerRutas(Convert.ToInt32(id_u));
                return Json(rutas, JsonRequestBehavior.AllowGet);
            }

            [Authorize]
            [HttpGet]
            public string GenerarCodigoRuta(string id)
            {
                var rutaId = Convert.ToInt64(id);
                var codigo = Guid.NewGuid().ToString("N")
                    .Substring(0, 6);

                var dbHelper = new DataBaseHelper(DataBaseHelper.GetConnection());
                var message = dbHelper.ActualizarCodigoRuta(rutaId, codigo);

                return codigo;
            }

            [Authorize]
            [HttpGet]
            public JsonResult ObtenerMovimientos(string id)
            {
                string str_initial_date = id.Split('_')[0];
                string str_final_date = id.Split('_')[1];

                DateTime initial_date, final_date;
                initial_date = str_initial_date == "null" ? new DateTime(2000, 1, 1)
                    : GetDateFromStr(str_initial_date);
                final_date = str_final_date == "null" ? DateTime.Now
                    : GetDateFromStr(str_final_date);
                
                var id_user = Convert.ToInt32(
                    HttpContext.GetOwinContext().Authentication.User.Claims
                        .FirstOrDefault(x => x.Type == ClaimTypes.UserData).Value);

                var dbHelper = new DataBaseHelper(DataBaseHelper.GetConnection());
                var items = dbHelper.ObtenerMovimientos(id_user, (DateTime)initial_date, (DateTime)final_date);

                return Json(items, JsonRequestBehavior.AllowGet); ;
            }
*/
            
			private DateTime GetDateFromStr(string str)
            {
                return new DateTime(
                       Convert.ToInt32(str.Split('-')[0]),
                       Convert.ToInt32(str.Split('-')[1]),
                       Convert.ToInt32(str.Split('-')[2]));
            }

			[HttpPost]
			public ActionResult Login(EmpresaUsuario item)
			{
				var dbHelper = new DataBaseHelper(DataBaseHelper.GetConnection());
				var id_user = 1;//dbHelper.ValidateLogin(item);
				var user_valid = id_user != 0;
				//return Json(false);
				if (user_valid)
				{
					var claims = new List<Claim>
					{
						new Claim(ClaimTypes.Name, item.Login),
						new Claim(ClaimTypes.UserData, id_user.ToString())
					};

					var userIdentity = new ClaimsIdentity(claims, "CookieAuthentication");
					HttpContext.GetOwinContext().Authentication.SignIn(userIdentity);

					return Json(true);
				}

				return Json(false);
			}


			[HttpGet]
			public bool Logout()
			{
				HttpContext.GetOwinContext().Authentication.SignOut("CookieAuthentication");
				return true;
			}

        }

        public class ObjetoJson
        {
            public string Id { get; set; }
            public string Nombre { get; set; }
        }
    
}