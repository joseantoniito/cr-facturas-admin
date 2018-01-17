using System;

namespace LibraryBapp
{
	public class FacturaManual
	{
		// FacturaManualId, EmpresaId, Registrado, UsuarioId, Consecutivo, Observaciones, 
		//EstadoFactura, Moneda, MontoTotal, MontoCobrado, MontoDescuento, MontoBruto, TipoPago, 
		//ArchivoXMLId, ClienteId

		public long FacturaManualId { get; set; }

		public int EmpresaId { get; set; }

		public DateTime Registrado { get; set; }

		public int UsuarioId { get; set; }

		public long Consecutivo { get; set; }

		public string Observaciones { get; set; }

		public int EstadoFactura { get; set; }

		public int Moneda { get; set; }

		public decimal MontoTotal { get; set; }

		public decimal MontoCobrado { get; set; }

		public decimal MontoDescuento { get; set; }

		public decimal MontoBruto { get; set; }

		public int TipoPago { get; set; }

		public long ArchivoXMLId { get; set; }

		public long ClienteId { get; set; }
	}
}

