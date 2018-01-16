using System;

namespace LibraryBapp
{
	public class Empleado
	{
		//EmpleadoId, EmpresaId, Nombre, Apellido1, Apellido2, Telefono1, Telefono2, Apodo, 
		//Identificacion, Foto, Horario, TipoPago, Puesto, Status

		public int EmpleadoId { get; set; }

		public int EmpresaId { get; set; }

		public string Nombre { get; set; }

		public string Apellido1 { get; set; }

		public string Apellido2 { get; set; }

		public string Telefono1 { get; set; }

		public string Telefono2 { get; set; }

		public string Apodo { get; set; }

		public string Identificacion { get; set; }

		public string Foto { get; set; }

		public string Horario { get; set; }

		public int TipoPago { get; set; }

		public string Puesto { get; set; }

		public int Status { get; set; }
	}
}

