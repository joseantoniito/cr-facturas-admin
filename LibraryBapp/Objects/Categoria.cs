using System;
using System.Text;

namespace LibraryBapp
{
	public class Categoria
	{
		//CategoriaId, Nombre, CategoríaPadreId, EmpresaId

		public int CategoriaId { get; set; }

		public string Nombre { get; set; }

		public int CategoríaPadreId { get; set; }

		public int EmpresaId { get; set; }
	}
}

