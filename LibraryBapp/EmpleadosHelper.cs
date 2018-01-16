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
	public class EmpleadosHelper : IDisposable
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

		public EmpleadosHelper(IDbConnection cn)
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
			


		public List<Empleado> ObtenerEmpleados(int id)
		{
			var strQuery =
				@"SELECT 
					EmpleadoId, 
					EmpresaId, 
					Nombre, 
					Apellido1, 
					Apellido2, 
					Telefono1, 
					Telefono2, 
					Apodo, 
					Identificacion, 
					Foto, 
					Horario, 
					TipoPago, 
					Puesto, 
					Status
				FROM CCSoftDB.dbo.Empleado
				WHERE EmpresaId = @id
                ";
			var list = 
				_cn.Query<Empleado>(
					strQuery,
					new { id }
				).ToList();
			return list;
		}

		public Empleado ObtenerEmpleado(int id)
		{
			var strQuery =
				@"SELECT 
					EmpleadoId, 
					EmpresaId, 
					Nombre, 
					Apellido1, 
					Apellido2, 
					Telefono1, 
					Telefono2, 
					Apodo, 
					Identificacion, 
					Foto, 
					Horario, 
					TipoPago, 
					Puesto, 
					Status
				FROM CCSoftDB.dbo.Empleado
				WHERE EmpleadoId = @id
                ";

			var item = _cn.Query<Empleado>(
				strQuery,
				new { id }
			).SingleOrDefault();

			return item;
		}

		public Empleado GuardarEmpleado(Empleado empleado)
		{
			//EmpleadoId, EmpresaId, Nombre, Apellido1, Apellido2, Telefono1, Telefono2, Apodo, 
			//Identificacion, Foto, Horario, TipoPago, Puesto, Status
			if (empleado.EmpleadoId == 0) {
				var empleadoId = _cn.Query<int> (
					@"INSERT INTO CCSoftDB.dbo.Empleado(
						EmpresaId, 
						Nombre, 
						Apellido1, 
						Apellido2, 
						Telefono1, 
						Telefono2, 
						Apodo, 
						Identificacion, 
						Foto, 
						Horario, 
						TipoPago, 
						Puesto, 
						Status
	                )
	                VALUES(
						@EmpresaId, 
						@Nombre, 
						@Apellido1, 
						@Apellido2, 
						@Telefono1, 
						@Telefono2, 
						@Apodo, 
						@Identificacion, 
						@Foto, 
						@Horario, 
						@TipoPago, 
						@Puesto, 
						@Status
	                )
	                SELECT CAST(SCOPE_IDENTITY() as int)",
					empleado
				).SingleOrDefault ();
				empleado.EmpleadoId = empleadoId;
			} 
			else {
				_cn.Execute(
					@"UPDATE CCSoftDB.dbo.Empleado
                    SET EmpresaId = @EmpresaId, 
						Nombre = @Nombre, 
						Apellido1 = @Apellido1, 
						Apellido2 = @Apellido2, 
						Telefono1 = @Telefono1, 
						Telefono2 = @Telefono2, 
						Apodo = @Apodo, 
						Identificacion = @Identificacion, 
						Foto = @Foto, 
						Horario = @Horario, 
						TipoPago = @TipoPago, 
						Puesto = @Puesto, 
						Status = @Status
                    WHERE EmpleadoId = @EmpleadoId",
					empleado);
			}

			return empleado;
		}
	}
}

