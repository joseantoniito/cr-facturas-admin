using System;

namespace LibraryBapp
{
	public class FacturaManualLinea
	{
		//FacturaManualId, FacturaManualLineaId, TipoItem, ObservacionAdicional, MontoLineaCobrado, 
		//PrecioUnitario, Cantidad, MontoLineaCalculado, Codigo, ItemId

		public long FacturaManualId { get; set; }

		public long FacturaManualLineaId { get; set; }

		public int TipoItem { get; set; }

		public string ObservacionAdicional { get; set; }

		public decimal MontoLineaCobrado { get; set; }

		public decimal PrecioUnitario { get; set; }

		public int Cantidad { get; set; }

		public decimal MontoLineaCalculado { get; set; }

		public string Codigo { get; set; }

		public long ItemId { get; set; }
	}
}

