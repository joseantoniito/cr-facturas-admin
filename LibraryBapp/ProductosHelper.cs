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
	public class ProductosHelper : IDisposable
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

		public ProductosHelper(IDbConnection cn)
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



		public List<Producto> ObtenerProductos(int id)
		{
			var strQuery =
				@"SELECT 
					ProductoId, 
					EmpresaId, 
					Nombre, 
					Codigo, 
					CodigoBarras, 
					ProveedorId, 
					TipoProductoId, 
					CategoriaId, 
					SubCategoriaId, 
					PrecioCompra, 
					PrecioFinal, 
					Existencias, 
					Impuesto, 
					VistoEnFacturacion
				FROM CCSoftDB.dbo.Producto
				WHERE EmpresaId = @id
                ";
			var list = 
				_cn.Query<Producto>(
					strQuery,
					new { id }
				).ToList();
			return list;
		}

		public Producto ObtenerProducto(int id)
		{
			var strQuery =
				@"SELECT 
					ProductoId, 
					EmpresaId, 
					Nombre, 
					Codigo, 
					CodigoBarras, 
					ProveedorId, 
					TipoProductoId, 
					CategoriaId, 
					SubCategoriaId, 
					PrecioCompra, 
					PrecioFinal, 
					Existencias, 
					Impuesto, 
					VistoEnFacturacion
				FROM CCSoftDB.dbo.Producto
				WHERE ProductoId = @id
                ";

			var item = _cn.Query<Producto>(
				strQuery,
				new { id }
			).SingleOrDefault();

			return item;
		}

		public Producto GuardarProducto(Producto item)
		{
			if (item.ProductoId == 0) {
				var id = _cn.Query<int> (
					@"INSERT INTO CCSoftDB.dbo.Producto(
						EmpresaId, 
						Nombre, 
						Codigo, 
						CodigoBarras, 
						ProveedorId, 
						TipoProductoId, 
						CategoriaId, 
						SubCategoriaId, 
						PrecioCompra, 
						PrecioFinal, 
						Existencias, 
						Impuesto, 
						VistoEnFacturacion
	                )
	                VALUES(
						@EmpresaId, 
						@Nombre, 
						@Codigo, 
						@CodigoBarras, 
						@ProveedorId, 
						@TipoProductoId, 
						@CategoriaId, 
						@SubCategoriaId, 
						@PrecioCompra, 
						@PrecioFinal, 
						@Existencias, 
						@Impuesto, 
						@VistoEnFacturacion
	                )
	                SELECT CAST(SCOPE_IDENTITY() as int)",
					item
				).SingleOrDefault ();
				item.ProductoId = id;
			} 
			else {
				_cn.Execute(
					@"UPDATE CCSoftDB.dbo.Producto
                    SET 
						EmpresaId = @EmpresaId, 
						Nombre = @Nombre, 
						Codigo = @Codigo, 
						CodigoBarras = @CodigoBarras, 
						ProveedorId = @ProveedorId, 
						TipoProductoId = @TipoProductoId, 
						CategoriaId = @CategoriaId, 
						SubCategoriaId = @SubCategoriaId, 
						PrecioCompra = @PrecioCompra, 
						PrecioFinal = @PrecioFinal, 
						Existencias = @Existencias, 
						Impuesto = @Impuesto, 
						VistoEnFacturacion = @VistoEnFacturacion
                    WHERE ProductoId = @ProductoId",
					item);
			}

			return item;
		}
	}
}

