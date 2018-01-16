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
	public class CitasHelper : IDisposable
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

		public CitasHelper(IDbConnection cn)
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


		public List<Agenda> ObtenerAgendas(int id)
		{
			//AgendaId, EmpresaId, ClienteId, EmpleadoId, ServicioId, Fecha, EstadoCita
			var strQuery =
				@"SELECT 
					AgendaId, 
					EmpresaId, 
					ClienteId, 
					EmpleadoId, 
					ServicioId, 
					Fecha, 
					EstadoCita
				FROM CCSoftDB.dbo.Agenda
				WHERE EmpresaId = @id
                ";
			var list = 
				_cn.Query<Agenda>(
					strQuery,
					new { id }
				).ToList();
			return list;
		}

		public Agenda ObtenerAgenda(int id)
		{
			var strQuery =
				@"SELECT 
					AgendaId, 
					EmpresaId, 
					ClienteId, 
					EmpleadoId, 
					ServicioId, 
					Fecha, 
					EstadoCita
				FROM CCSoftDB.dbo.Agenda
				WHERE AgendaId = @id
                ";

			var item = _cn.Query<Agenda>(
				strQuery,
				new { id }
			).SingleOrDefault();

			return item;
		}

		public Agenda GuardarAgenda(Agenda item)
		{
			if (item.AgendaId == 0) {
				var id = _cn.Query<int> (
					@"INSERT INTO CCSoftDB.dbo.Agenda(
						EmpresaId, 
						ClienteId, 
						EmpleadoId, 
						ServicioId, 
						Fecha, 
						EstadoCita
	                )
	                VALUES(
						@EmpresaId, 
						@ClienteId, 
						@EmpleadoId, 
						@ServicioId, 
						@Fecha, 
						@EstadoCita
	                )
	                SELECT CAST(SCOPE_IDENTITY() as int)",
					item
				).SingleOrDefault ();
				item.AgendaId = id;
			} 
			else {
				_cn.Execute(
					@"UPDATE CCSoftDB.dbo.Agenda
                    SET  
						EmpresaId = @EmpresaId, 
						ClienteId = @ClienteId, 
						EmpleadoId = @EmpleadoId, 
						ServicioId = @ServicioId, 
						Fecha = @Fecha, 
						EstadoCita = @EstadoCita
                    WHERE AgendaId = @AgendaId",
					item);
			}

			return item;
		}



		public List<HistorialVisita> ObtenerHistorialVisitas(int id)
		{
			//HistorialVisitaId, ClienteId, EmpresaId, AtendidoPor, FechaIngreso, FechaSalida, Observaciones
			var strQuery =
				@"SELECT 
					HistorialVisitaId, 
					ClienteId, 
					EmpresaId, 
					AtendidoPor, 
					FechaIngreso, 
					FechaSalida, 
					Observaciones
				FROM CCSoftDB.dbo.HistorialVisita
				WHERE EmpresaId = @id
                ";
			var list = 
				_cn.Query<HistorialVisita>(
					strQuery,
					new { id }
				).ToList();
			return list;
		}

		public HistorialVisita ObtenerHistorialVisita(int id)
		{
			var strQuery =
				@"SELECT 
					HistorialVisitaId, 
					ClienteId, 
					EmpresaId, 
					AtendidoPor, 
					FechaIngreso, 
					FechaSalida, 
					Observaciones
				FROM CCSoftDB.dbo.HistorialVisita
				WHERE HistorialVisitaId = @id
                ";

			var item = _cn.Query<HistorialVisita>(
				strQuery,
				new { id }
			).SingleOrDefault();

			return item;
		}

		public HistorialVisita GuardarHistorialVisita(HistorialVisita item)
		{
			if (item.HistorialVisitaId == 0) {
				var id = _cn.Query<int> (
					@"INSERT INTO CCSoftDB.dbo.HistorialVisita(
						ClienteId, 
						EmpresaId, 
						AtendidoPor, 
						FechaIngreso, 
						FechaSalida, 
						Observaciones
	                )
	                VALUES(
						@ClienteId, 
						@EmpresaId, 
						@AtendidoPor, 
						@FechaIngreso, 
						@FechaSalida, 
						@Observaciones
	                )
	                SELECT CAST(SCOPE_IDENTITY() as int)",
					item
				).SingleOrDefault ();
				item.HistorialVisitaId = id;
			} 
			else {
				_cn.Execute(
					@"UPDATE CCSoftDB.dbo.HistorialVisita
                    SET  
						ClienteId = @ClienteId, 
						EmpresaId = @EmpresaId, 
						AtendidoPor = @AtendidoPor, 
						FechaIngreso = @FechaIngreso, 
						FechaSalida = @FechaSalida, 
						Observaciones = @Observaciones
                    WHERE HistorialVisitaId = @HistorialVisitaId",
					item);
			}

			return item;
		}
	}
}

