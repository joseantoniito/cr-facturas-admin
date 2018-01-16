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
	public class ClientesHelper : IDisposable
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

		public ClientesHelper(IDbConnection cn)
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



		public List<Cliente> ObtenerClientes(int id)
		{
			var strQuery =
				@"SELECT 
					ClienteId, 
					EmpresaId, 
					Nombre, 
					Apellido1, 
					Apellido2, 
					Telefono1, 
					Telefono2, 
					Telefono3, 
					Correo,
					Cumpleaños,
					RecomendadoPorClienteId,
					Status,
					CreditoDisponible
				FROM CCSoftDB.dbo.Cliente
				WHERE EmpresaId = @id
                ";
			var list = 
				_cn.Query<Cliente>(
					strQuery,
					new { id }
				).ToList();
			return list;
		}

		public Cliente ObtenerCliente(int id)
		{
			var strQuery =
				@"SELECT 
					ClienteId, 
					EmpresaId, 
					Nombre, 
					Apellido1, 
					Apellido2, 
					Telefono1, 
					Telefono2, 
					Telefono3, 
					Correo,
					Cumpleaños,
					RecomendadoPorClienteId,
					Status,
					CreditoDisponible
				FROM CCSoftDB.dbo.Cliente
				WHERE ClienteId = @id
                ";

			var item = _cn.Query<Cliente>(
				strQuery,
				new { id }
			).SingleOrDefault();

			return item;
		}

		public Cliente GuardarCliente(Cliente item)
		{
			if (item.ClienteId == 0) {
				var id = _cn.Query<int> (
					@"INSERT INTO CCSoftDB.dbo.Cliente(
						EmpresaId, 
						Nombre, 
						Apellido1, 
						Apellido2, 
						Telefono1, 
						Telefono2, 
						Telefono3, 
						Correo,
						Cumpleaños,
						RecomendadoPorClienteId,
						Status,
						CreditoDisponible
	                )
	                VALUES(
						@EmpresaId, 
						@Nombre, 
						@Apellido1, 
						@Apellido2, 
						@Telefono1, 
						@Telefono2, 
						@Telefono3, 
						@Correo,
						@Cumpleaños,
						@RecomendadoPorClienteId,
						@Status,
						@CreditoDisponible
	                )
	                SELECT CAST(SCOPE_IDENTITY() as int)",
					item
				).SingleOrDefault ();
				item.ClienteId = id;
			} 
			else {
				_cn.Execute(
					@"UPDATE CCSoftDB.dbo.Cliente
                    SET  
						EmpresaId = @EmpresaId, 
						Nombre = @Nombre, 
						Apellido1 = @Apellido1, 
						Apellido2 = @Apellido2, 
						Telefono1 = @Telefono1, 
						Telefono2 = @Telefono2, 
						Telefono3 = @Telefono3, 
						Correo = @Correo,
						Cumpleaños = @Cumpleaños,
						RecomendadoPorClienteId = @RecomendadoPorClienteId,
						Status = @Status,
						CreditoDisponible = @CreditoDisponible
                    WHERE ClienteId = @ClienteId",
					item);
			}

			return item;
		}
	}
}

