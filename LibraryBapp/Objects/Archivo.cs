using System;

namespace LibraryBapp
{
	public class Archivo
	{
		//ArchivoId, EmpresaId, Ruta, StorageId, Registrado, EstadoArchivo, Hash, LlaveHacienda, 
		//TipoArchivo, UsuarioId, CertificadoId, ReceptorId

		public int ArchivoId { get; set; }

		public int EmpresaId { get; set; }

		public string Ruta { get; set; }

		public int StorageId { get; set; }

		public DateTime Registrado { get; set; }

		public int EstadoArchivo { get; set; }

		public string Hash { get; set; }

		public string LlaveHacienda { get; set; }

		public int TipoArchivo { get; set; }

		public int UsuarioId { get; set; }

		public int CertificadoId { get; set; }

		public long ReceptorId { get; set; }
	}
}

