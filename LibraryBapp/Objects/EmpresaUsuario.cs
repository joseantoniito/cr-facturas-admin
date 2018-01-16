using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBapp
{
	public class EmpresaUsuario
	{
		public int EmpresaUsuarioId { get; set; }
		public long EmpresaId { get; set; }
		public string Login { get; set; }
		public string Clave { get; set; }
		public DateTime Registered { get; set; }
		public int Status { get; set; }
		public string Permisos { get; set; }
	}
}
