using System;

namespace LibraryBapp
{
	public class Servicio
	{
		//ServicioId, EmpresaId, FamiliaId, DisponibleEnCitas, Precio, TiempoDuracionMin, Impuesto,
		//Costo, Detalle

		public int ServicioId { get; set; }

		public int EmpresaId { get; set; }

		public int FamiliaId { get; set; }

		public bool DisponibleEnCitas { get; set; }

		public decimal Precio { get; set; }

		public int TiempoDuracionMin { get; set; }

		public decimal Impuesto { get; set; }

		public decimal Costo { get; set; }

		public string Detalle { get; set; }
	}
}

