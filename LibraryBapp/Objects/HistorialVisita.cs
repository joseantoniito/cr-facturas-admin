using System;

namespace LibraryBapp
{
	public class HistorialVisita
	{
		//HistorialVisitaId, ClienteId, EmpresaId, AtendidoPor, FechaIngreso, FechaSalida, Observaciones

		public int HistorialVisitaId { get; set; }

		public int ClienteId { get; set; }

		public int EmpresaId { get; set; }

		public int AtendidoPor { get; set; }

		public DateTime FechaIngreso { get; set; }

		public DateTime FechaSalida { get; set; }

		public string Observaciones { get; set; }
	}
}

