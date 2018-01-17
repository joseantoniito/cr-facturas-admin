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
	public class FacturacionHelper : IDisposable
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

		public FacturacionHelper(IDbConnection cn)
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


		public List<FacturaManual> ObtenerFacturaManuals(int id)
		{
			var strQuery =
				@"SELECT 
					FacturaManualId, 
					EmpresaId, 
					Registrado, 
					UsuarioId, 
					Consecutivo, 
					Observaciones, 
					EstadoFactura,
					Moneda, 
					MontoTotal, 
					MontoCobrado, 
					MontoDescuento, 
					MontoBruto, 
					TipoPago, 
					ArchivoXMLId, 
					ClienteId
				FROM FactElect.dbo.FacturaManual
				WHERE EmpresaId = @id
                ";
			var list = 
				_cn.Query<FacturaManual>(
					strQuery,
					new { id }
				).ToList();
			return list;
		}

		public FacturaManual ObtenerFacturaManual(int id)
		{
			var strQuery =
				@"SELECT 
					FacturaManualId, 
					EmpresaId, 
					Registrado, 
					UsuarioId, 
					Consecutivo, 
					Observaciones, 
					EstadoFactura,
					Moneda, 
					MontoTotal, 
					MontoCobrado, 
					MontoDescuento, 
					MontoBruto, 
					TipoPago, 
					ArchivoXMLId, 
					ClienteId
				FROM FactElect.dbo.FacturaManual
				WHERE FacturaManualId = @id
                ";

			var item = _cn.Query<FacturaManual>(
				strQuery,
				new { id }
			).SingleOrDefault();

			return item;
		}

		public FacturaManual GuardarFacturaManual(FacturaManual item)
		{
			if (item.FacturaManualId == 0) {
				var id = _cn.Query<int> (
					@"INSERT INTO FactElect.dbo.FacturaManual(
						EmpresaId, 
						Registrado, 
						UsuarioId, 
						Consecutivo, 
						Observaciones, 
						EstadoFactura,
						Moneda, 
						MontoTotal, 
						MontoCobrado, 
						MontoDescuento, 
						MontoBruto, 
						TipoPago, 
						ArchivoXMLId, 
						ClienteId
	                )
	                VALUES(
						@EmpresaId, 
						@Registrado, 
						@UsuarioId, 
						@Consecutivo, 
						@Observaciones, 
						@EstadoFactura,
						@Moneda, 
						@MontoTotal, 
						@MontoCobrado, 
						@MontoDescuento, 
						@MontoBruto, 
						@TipoPago, 
						@ArchivoXMLId, 
						ClienteId
	                )
	                SELECT CAST(SCOPE_IDENTITY() as int)",
					item
				).SingleOrDefault ();
				item.FacturaManualId = id;
			} 
			else {
				_cn.Execute(
					@"UPDATE FactElect.dbo.FacturaManual
                    SET 
						EmpresaId = @EmpresaId, 
						Registrado = @Registrado, 
						UsuarioId = @UsuarioId, 
						Consecutivo = @Consecutivo, 
						Observaciones = @Observaciones, 
						EstadoFactura = @EstadoFactura,
						Moneda = @Moneda, 
						MontoTotal = @MontoTotal, 
						MontoCobrado = @MontoCobrado, 
						MontoDescuento = @MontoDescuento, 
						MontoBruto = @MontoBruto, 
						TipoPago = @TipoPago, 
						ArchivoXMLId = @ArchivoXMLId, 
						ClienteId = @ClienteId
                    WHERE FacturaManualId = @FacturaManualId",
					item);
			}

			return item;
		}



		public List<FacturaManualLinea> ObtenerFacturaManualLineas(int id)
		{
			//FacturaManualId, FacturaManualLineaId, TipoItem, ObservacionAdicional, MontoLineaCobrado, 
			//PrecioUnitario, Cantidad, MontoLineaCalculado, Codigo, ItemId

			var strQuery =
				@"SELECT 
					FacturaManualId, 
					FacturaManualLineaId, 
					TipoItem, 
					ObservacionAdicional, 
					MontoLineaCobrado, 
					PrecioUnitario, 
					Cantidad,
					MontoLineaCalculado, 
					Codigo, 
					ItemId
				FROM FactElect.dbo.FacturaManualLinea
				WHERE EmpresaId = @id
                ";
			var list = 
				_cn.Query<FacturaManualLinea>(
					strQuery,
					new { id }
				).ToList();
			return list;
		}

		public FacturaManualLinea ObtenerFacturaManualLinea(int id)
		{
			var strQuery =
				@"SELECT 
					FacturaManualId, 
					FacturaManualLineaId, 
					TipoItem, 
					ObservacionAdicional, 
					MontoLineaCobrado, 
					PrecioUnitario, 
					Cantidad,
					MontoLineaCalculado, 
					Codigo, 
					ItemId
				FROM FactElect.dbo.FacturaManualLinea
				WHERE FacturaManualLineaId = @id
                ";

			var FacturaManualLinea = _cn.Query<FacturaManualLinea>(
				strQuery,
				new { id }
			).SingleOrDefault();

			return FacturaManualLinea;
		}

		public FacturaManualLinea GuardarFacturaManualLinea(FacturaManualLinea FacturaManualLinea)
		{
			if (FacturaManualLinea.FacturaManualLineaId == 0) {
				var id = _cn.Query<int> (
					@"INSERT INTO FactElect.dbo.FacturaManualLinea(
						@
	                )
	                VALUES(
						@FacturaManualId, 
						@TipoItem, 
						@ObservacionAdicional, 
						@MontoLineaCobrado, 
						@PrecioUnitario, 
						@Cantidad,
						@MontoLineaCalculado, 
						@Codigo, 
						@ItemId
	                )
	                SELECT CAST(SCOPE_IDENTITY() as int)",
					FacturaManualLinea
				).SingleOrDefault ();
				FacturaManualLinea.FacturaManualLineaId = id;
			} 
			else {
				_cn.Execute(
					@"UPDATE FactElect.dbo.FacturaManualLinea
                    SET 
						FacturaManualId = @FacturaManualId,
						TipoItem = @TipoItem, 
						ObservacionAdicional = @ObservacionAdicional, 
						MontoLineaCobrado = @MontoLineaCobrado, 
						PrecioUnitario = @PrecioUnitario, 
						Cantidad = @Cantidad,
						MontoLineaCalculado = @MontoLineaCalculado, 
						Codigo = @Codigo, 
						ItemId = @ItemId
                    WHERE FacturaManualLineaId = @FacturaManualLineaId",
					FacturaManualLinea);
			}

			return FacturaManualLinea;
		}
	}
}

