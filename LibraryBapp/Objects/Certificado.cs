using System;
using System.Text;

namespace LibraryBapp
{
	public class Certificado
	{
		//CertificadoId, EmpresaId, RutaCertificado, Registrado, Status, ClaveArchivo

		public int CertificadoId { get; set; }

		public int EmpresaId { get; set; }

		public string RutaCertificado { get; set; }

		public DateTime Registrado { get; set; }

		public int Status { get; set; }

		public string ClaveArchivo { get; set; }
	}
}

