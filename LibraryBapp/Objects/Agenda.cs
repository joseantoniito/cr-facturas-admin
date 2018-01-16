using System;

namespace LibraryBapp
{
	public class Agenda
	{
		//AgendaId, EmpresaId, ClienteId, EmpleadoId, ServicioId, Fecha, EstadoCita

		public int AgendaId { get; set; }

		public int EmpresaId { get; set; }

		public int ClienteId { get; set; }

		public int EmpleadoId { get; set; }

		public int ServicioId { get; set; }

		public DateTime Fecha { get; set; }

		public int EstadoCita { get; set; }
	}
}

