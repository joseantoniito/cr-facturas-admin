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
	public class ItemsHelper : IDisposable
	{
		IDbConnection _cn;
		public static IDbConnection GetConnection()
		{
			//var connection_str = ConfigurationManager.ConnectionStrings["CN"].ConnectionString;
			var connection_str = 
				@"Data Source=PERSONAL\SQLEXPRESS;Initial Catalog=cash_app;User ID=sa;Password=0nly-n0is3; Persist Security Info=True;";

			var connection_str_1 =
				@"data source=www.ccmvservices.com, 36754;initial catalog=FactElect;persist security info=True;user id=CCSoft;password=abcd.1234;";
			return new SqlConnection( connection_str_1);
		}

		public ItemsHelper(IDbConnection cn)
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



		public List<Item> ObtenerItems(int id)
		{
			var strQuery =
				@"SELECT 
					ItemId, 
					EmpresaId, 
					TipoItem, 
					Descripcion, 
					MontoItem, 
					Moneda, 
					Activo
				FROM FactElect.dbo.Item
				WHERE EmpresaId = @id
                ";
			var list = 
				_cn.Query<Item>(
					strQuery,
					new { id }
				).ToList();
			return list;
		}

		public Item ObtenerItem(int id)
		{
			var strQuery =
				@"SELECT 
					ItemId, 
					EmpresaId, 
					TipoItem, 
					Descripcion, 
					MontoItem, 
					Moneda, 
					Activo
				FROM FactElect.dbo.Item
				WHERE ItemId = @id
                ";

			var item = _cn.Query<Item>(
				strQuery,
				new { id }
			).SingleOrDefault();

			return item;
		}

		public Item GuardarItem(Item item)
		{
			if (item.ItemId == 0) {
				var id = _cn.Query<int> (
					@"INSERT INTO FactElect.dbo.Item(
						EmpresaId, 
						TipoItem, 
						Descripcion, 
						MontoItem, 
						Moneda, 
						Activo
	                )
	                VALUES(
						@EmpresaId, 
						@TipoItem, 
						@Descripcion, 
						@MontoItem, 
						@Moneda, 
						@Activo
	                )
	                SELECT CAST(SCOPE_IDENTITY() as int)",
					item
				).SingleOrDefault ();
				item.ItemId = id;
			} 
			else {
				_cn.Execute(
					@"UPDATE FactElect.dbo.Item
                    SET 
						EmpresaId = @EmpresaId, 
						TipoItem = @TipoItem, 
						Descripcion = @Descripcion, 
						MontoItem = @MontoItem, 
						Moneda = @Moneda, 
						Activo = @Activo
                    WHERE ItemId = @ItemId",
					item);
			}

			return item;
		}
	}
}

