using System;

namespace LibraryBapp
{
	public class Item
	{
		//ItemId, EmpresaId, TipoItem, Descripcion, MontoItem, Moneda, Activo

		public long ItemId { get; set; }

		public int EmpresaId { get; set; }

		public int TipoItem { get; set; }

		public string Descripcion { get; set; }

		public decimal MontoItem { get; set; }

		public int Moneda { get; set; }

		public bool Activo { get; set; }

	}
}

