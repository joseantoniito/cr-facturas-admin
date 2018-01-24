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
	public class ArchivoXmlHelper : IDisposable
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

		public ArchivoXmlHelper(IDbConnection cn)
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
			


		public List<Archivo> ObtenerArchivos(int id)
		{
			//ArchivoId, EmpresaId, Ruta, StorageId, Registrado, EstadoArchivo, Hash, LlaveHacienda, 
			//TipoArchivo, UsuarioId, CertificadoId, ReceptorId
			var strQuery =
				@"SELECT 
					ArchivoId, 
					EmpresaId, 
					Ruta, 
					StorageId, 
					Registrado, 
					EstadoArchivo, 
					Hash, 
					LlaveHacienda, 
					TipoArchivo, 
					UsuarioId, 
					CertificadoId, 
					ReceptorId
				FROM FactElect.dbo.Archivo
				WHERE EmpresaId = @id
                ";
			var list = 
				_cn.Query<Archivo>(
					strQuery,
					new { id }
				).ToList();
			return list;
		}

		public Archivo ObtenerArchivo(int id)
		{
			var strQuery =
				@"SELECT 
					ArchivoId, 
					EmpresaId, 
					Ruta, 
					StorageId, 
					Registrado, 
					EstadoArchivo, 
					Hash, 
					LlaveHacienda, 
					TipoArchivo, 
					UsuarioId, 
					CertificadoId, 
					ReceptorId
				FROM FactElect.dbo.Archivo
				WHERE ArchivoId = @id
                ";

			var item = _cn.Query<Archivo>(
				strQuery,
				new { id }
			).SingleOrDefault();

			return item;
		}

		public Archivo GuardarArchivo(Archivo archivo)
		{

			if (archivo.ArchivoId == 0) {
				var archivoId = _cn.Query<int> (
					@"INSERT INTO FactElect.dbo.Archivo(
						EmpresaId, 
						Ruta, 
						StorageId, 
						Registrado, 
						EstadoArchivo, 
						Hash, 
						LlaveHacienda, 
						TipoArchivo, 
						UsuarioId, 
						CertificadoId, 
						ReceptorId
	                )
	                VALUES(
						@EmpresaId, 
						@Ruta, 
						@StorageId, 
						@Registrado, 
						@EstadoArchivo, 
						@Hash, 
						@LlaveHacienda, 
						@TipoArchivo, 
						@UsuarioId, 
						@CertificadoId, 
						@ReceptorId
	                )
	                SELECT CAST(SCOPE_IDENTITY() as int)",
					archivo
				).SingleOrDefault ();
				archivo.ArchivoId = archivoId;
			} 
			else {
				_cn.Execute(
					@"UPDATE FactElect.dbo.Archivo
                    SET 
						EmpresaId = @EmpresaId, 
						Ruta = @Ruta, 
						StorageId = @StorageId, 
						Registrado = @Registrado, 
						EstadoArchivo = @EstadoArchivo, 
						Hash = @Hash, 
						LlaveHacienda = @LlaveHacienda, 
						TipoArchivo = @TipoArchivo, 
						UsuarioId = @UsuarioId, 
						CertificadoId = @CertificadoId, 
						ReceptorId = @ReceptorId
                    WHERE ArchivoId = @ArchivoId",
					archivo);
			}

			return archivo;
		}

	}
}

