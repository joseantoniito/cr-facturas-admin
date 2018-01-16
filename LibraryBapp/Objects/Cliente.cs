using System;

namespace LibraryBapp
{
	public class Cliente
	{
		//ClienteId, EmpresaId, Nombre, Apellido1, Apellido2, Telefono1, Telefono2, Telefono3, 
		//Correo, Cumpleaños, RecomendadoPorClienteId, Status, CreditoDisponible

		public int ClienteId { get; set; }

		public int EmpresaId { get; set; }

		public string Nombre { get; set; }

		public string Apellido1 { get; set; }

		public string Apellido2 { get; set; }

		public string Telefono1 { get; set; }

		public string Telefono2 { get; set; }

		public string Telefono3 { get; set; }

		public string Correo { get; set; }

		public DateTime Cumpleaños { get; set; }

		public int RecomendadoPorClienteId { get; set; }

		public int Status { get; set; }

		public decimal CreditoDisponible { get; set; }
	}
}

