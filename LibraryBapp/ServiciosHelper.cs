using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Dapper;
using System.Data.SqlClient;
using System.Configuration;

namespace LibraryBapp
{
	public class ServiciosHelper : IDisposable
	{
		IDbConnection _cn;
		public static IDbConnection GetConnection()
		{
			//var connection_str = ConfigurationManager.ConnectionStrings["CN"].ConnectionString;
			var connection_str = 
				@"Data Source=PERSONAL\SQLEXPRESS;Initial Catalog=cash_app;User ID=sa;Password=0nly-n0is3; Persist Security Info=True;";

			var connection_str_1 =
				@"data source=www.ccmvservices.com, 36754;initial catalog=CCSoftDB;persist security info=True;user id=CCSoft;password=abcd.1234;";
			return new SqlConnection( connection_str_1);
		}

		public ServiciosHelper(IDbConnection cn)
		{
			_cn = cn;
		}

		public void Open()
		{
			//Aqui se debe leer la cadena de conexion desde el archivo de configuracion
			_cn.Open(); 
		}

		public void Dispose()
		{
			if (_cn.State == ConnectionState.Open)
				_cn.Close();
		}



		public List<Servicio> ObtenerServicios(int id)
		{
			var strQuery =
				@"SELECT 
					ServicioId, 
					EmpresaId, 
					FamiliaId, 
					DisponibleEnCitas, 
					Precio, 
					TiempoDuracionMin, 
					Impuesto, 
					Costo, 
					Detalle
				FROM CCSoftDB.dbo.Servicio
				WHERE EmpresaId = @id
                ";
			var list = 
				_cn.Query<Servicio>(
					strQuery,
					new { id }
				).ToList();
			return list;
		}

		public Servicio ObtenerServicio(int id)
		{
			var strQuery =
				@"SELECT 
					ServicioId, 
					EmpresaId, 
					FamiliaId, 
					DisponibleEnCitas, 
					Precio, 
					TiempoDuracionMin, 
					Impuesto, 
					Costo, 
					Detalle
				FROM CCSoftDB.dbo.Servicio
				WHERE ServicioId = @id
                ";

			var item = _cn.Query<Servicio>(
				strQuery,
				new { id }
			).SingleOrDefault();

			return item;
		}

		public Servicio GuardarServicio(Servicio item)
		{
			if (item.ServicioId == 0) {
				var id = _cn.Query<int> (
					@"INSERT INTO CCSoftDB.dbo.Servicio(
						EmpresaId, 
						FamiliaId, 
						DisponibleEnCitas, 
						Precio, 
						TiempoDuracionMin, 
						Impuesto, 
						Costo, 
						Detalle
	                )
	                VALUES(
						@EmpresaId, 
						@FamiliaId, 
						@DisponibleEnCitas, 
						@Precio, 
						@TiempoDuracionMin, 
						@Impuesto, 
						@Costo, 
						@Detalle
	                )
	                SELECT CAST(SCOPE_IDENTITY() as int)",
					item
				).SingleOrDefault ();
				item.ServicioId = id;
			} 
			else {
				_cn.Execute(
					@"UPDATE CCSoftDB.dbo.Servicio
                    SET  
						EmpresaId = @EmpresaId, 
						FamiliaId = @FamiliaId, 
						DisponibleEnCitas = @DisponibleEnCitas, 
						Precio = @Precio, 
						TiempoDuracionMin = @TiempoDuracionMin, 
						Impuesto = @Impuesto, 
						Costo = @Costo, 
						Detalle = @Detalle
                    WHERE ServicioId = @ServicioId",
					item);
			}

			return item;
		}
	}
}

