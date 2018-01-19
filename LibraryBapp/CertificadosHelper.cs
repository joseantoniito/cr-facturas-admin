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
	public class CertificadosHelper : IDisposable
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

		public CertificadosHelper(IDbConnection cn)
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



		public List<Certificado> ObtenerCertificados(int id)
		{
			//CertificadoId, EmpresaId, RutaCertificado, Registrado, Status, ClaveArchivo
			var strQuery =
				@"SELECT 
					CertificadoId, 
					EmpresaId, 
					RutaCertificado, 
					Registrado, 
					Status, 
					ClaveArchivo
				FROM FactElect.dbo.Certificado
				WHERE EmpresaId = @id
                ";
			var list = 
				_cn.Query<Certificado>(
					strQuery,
					new { id }
				).ToList();
			return list;
		}

		public Certificado ObtenerCertificado(int id)
		{
			var strQuery =
				@"SELECT 
					CertificadoId, 
					EmpresaId, 
					RutaCertificado, 
					Registrado, 
					Status, 
					ClaveArchivo
				FROM FactElect.dbo.Certificado
				WHERE CertificadoId = @id
                ";

			var item = _cn.Query<Certificado>(
				strQuery,
				new { id }
			).SingleOrDefault();

			return item;
		}

		public Certificado GuardarCertificado(Certificado certificado)
		{
			if (certificado.CertificadoId == 0) {
				var certificadoId = _cn.Query<int> (
					@"INSERT INTO FactElect.dbo.Certificado(
						EmpresaId, 
						RutaCertificado, 
						Registrado, 
						Status, 
						ClaveArchivo
	                )
	                VALUES(
						@EmpresaId, 
						@RutaCertificado, 
						@Registrado, 
						@Status, 
						@ClaveArchivo
	                )
	                SELECT CAST(SCOPE_IDENTITY() as int)",
					certificado
				).SingleOrDefault ();
				certificado.CertificadoId = certificadoId;
			} 
			else {
				_cn.Execute(
					@"UPDATE FactElect.dbo.Certificado
                    SET 
						EmpresaId = @EmpresaId, 
						RutaCertificado = @RutaCertificado, 
						Registrado = @Registrado, 
						Status = @Status, 
						ClaveArchivo = @ClaveArchivo
                    WHERE CertificadoId = @CertificadoId",
					certificado);
			}

			return certificado;
		}
	}
}

