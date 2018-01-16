using System;

namespace LibraryBapp
{
	public class Producto
	{
		//ProductoId, EmpresaId, Nombre, Codigo, CodigoBarras, ProveedorId, TipoProductoId, 
		//CategoriaId, SubCategoriaId, PrecioCompra, PrecioFinal, Existencias, Impuesto, VistoEnFacturacion

		public int ProductoId { get; set; }

		public int EmpresaId { get; set; }

		public string Nombre { get; set; }

		public string Codigo { get; set; }

		public string CodigoBarras { get; set; }

		public int ProveedorId { get; set; }

		public int TipoProductoId { get; set; }

		public int CategoriaId { get; set; }

		public int SubCategoriaId { get; set; }

		public decimal PrecioCompra { get; set; }

		public decimal PrecioFinal { get; set; }

		public int Existencias { get; set; }

		public decimal Impuesto { get; set; }

		public decimal VistoEnFacturacion { get; set; }
	}
}

