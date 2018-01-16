using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Dapper;
using System.Data.SqlClient;
using System.Configuration;
//using System.Web.Configuration;

namespace LibraryBapp
{
    public class DataBaseHelper : IDisposable
    {
        IDbConnection _cn;
        public static IDbConnection GetConnection()
        {
            //var connection_str = ConfigurationManager.ConnectionStrings["CN"].ConnectionString;
            var connection_str = 
@"Data Source=PERSONAL\SQLEXPRESS;Initial Catalog=cash_app;User ID=sa;Password=0nly-n0is3; Persist Security Info=True;";

            var connection_str_1 =
				@"data source=www.ccmvservices.com, 36754;initial catalog=CCSoftDB;persist security info=True;user id=CCSoft;password=abcd.1234;";
            return new SqlConnection( connection_str_1);
        }

        public DataBaseHelper(IDbConnection cn)
	    {
            _cn = cn;
	    }



		public List<Empresa> ObtenerEmpresas()
		{
			var strQuery =
				@"SELECT EmpresaId, 
					Nombre, 
					Telefono, 
					Direccion, 
					Status
				FROM CCSoftDB.dbo.Empresa
                ";
			//WHERE u.EmpresaUsuarioId = @empresaUsuarioId
			var list = _cn.Query<Empresa>(
				strQuery,
				new { }
			).ToList();

			return list;
		}

		public Empresa ObtenerEmpresa(int id)
		{
			var strQuery =
				@"SELECT EmpresaId, 
					Nombre, 
					Telefono, 
					Direccion, 
					Status
				FROM CCSoftDB.dbo.Empresa
				WHERE EmpresaId = @id
                ";
			
			var item = _cn.Query<Empresa>(
				strQuery,
				new { id }
			).SingleOrDefault();

			return item;
		}

		public Empresa GuardarEmpresa(Empresa empresa)
		{
			if (empresa.EmpresaId == 0) {
				empresa.Status = 1; //definir enum de status
				var empresaId = _cn.Query<int> (
					@"INSERT INTO CCSoftDB.dbo.Empresa(
	                    Nombre,
	                    Telefono,
						Direccion, 
	                    Status
	                )
	                VALUES(
	                    @Nombre, 
	                    @Telefono, 
	                    @Direccion, 
	                    @Status
	                )
	                SELECT CAST(SCOPE_IDENTITY() as int)",
	                empresa
                ).SingleOrDefault ();
				empresa.EmpresaId = empresaId;
			} 
			else {
				_cn.Execute(
					@"UPDATE CCSoftDB.dbo.Empresa
                    SET Nombre = @Nombre,
						Telefono = @Telefono,
						Direccion = @Direccion,
						Status = @Status
                    WHERE EmpresaId = @EmpresaId",
					empresa);
			}
				
			return empresa;
		}



		public List<Categoria> ObtenerCategorias(int empresaId)
		{
			var strQuery =
				@"SELECT 
					CategoriaId, 
					Nombre, 
					CategoríaPadreId, 
					EmpresaId
				FROM CCSoftDB.dbo.Categoria
				WHERE EmpresaId = @empresaId
                ";
			var list = 
				_cn.Query<Categoria>(
					strQuery,
					new { empresaId }
				).ToList();
			return list;
		}

		public Categoria ObtenerCategoria(int id)
		{
			var strQuery =
				@"SELECT 
					CategoriaId, 
					Nombre, 
					CategoríaPadreId, 
					EmpresaId
				FROM CCSoftDB.dbo.Categoria
				WHERE CategoriaId = @id
                ";

			var item = _cn.Query<Categoria>(
				strQuery,
				new { id }
			).SingleOrDefault();

			return item;
		}

		public Categoria GuardarCategoria(Categoria categoria)
		{
			//CategoriaId, Nombre, CategoríaPadreId, EmpresaId
			if (categoria.CategoriaId == 0) {
				var categoriaId = _cn.Query<int> (
					@"INSERT INTO CCSoftDB.dbo.Categoria(
	                    Nombre,
	                    CategoríaPadreId,
						EmpresaId
	                )
	                VALUES(
	                    @Nombre, 
	                    @CategoríaPadreId, 
	                    @EmpresaId
	                )
	                SELECT CAST(SCOPE_IDENTITY() as int)",
					categoria
				).SingleOrDefault ();
				categoria.CategoriaId = categoriaId;
			} 
			else {
				_cn.Execute(
					@"UPDATE CCSoftDB.dbo.Categoria
                    SET Nombre = @Nombre,
						CategoríaPadreId = @CategoríaPadreId,
						EmpresaId = @EmpresaId
                    WHERE CategoriaId = @CategoriaId",
					categoria);
			}

			return categoria;
		}



		/*FUNCIONES ANTERIORES DE REFERENCIA*/

       public long ValidateLogin(EmpresaUsuario user){
           //todo: corregir funcion
           var item =
                _cn.Query<EmpresaUsuario>(
                    @"SELECT EmpresaUsuarioId
                    FROM [PaymentDB].[dbo].[EmpresaUsuario]
                    WHERE Clave = @Clave
                    AND Login = @Login",
                    new { user.Clave, user.Login }
                ).SingleOrDefault();
           
           if(item == null)
                return 0;
            else
               return item.EmpresaUsuarioId;
       }

        public long SaveUser(User user){
            /*
                    usrVO = new Table_User();
                    usrVO.FirstName = req.Nombre;
                    usrVO.LastName = req.Apellido;
                    usrVO.Registered = DateTime.Now;
                    usrVO.Status = (int) UserStatusEnum.Activo;
                    usrVO.UserIdentity = req.Identificacion;
                    usrVO.UserKey = req.IMEI;
                    usrVO.UserTypeId = 0; //TODO: Definir la enumeracion UserType
                    usrVO.InternalKey = Guid.NewGuid().ToString("N");
             */

            /*_cn.Execute(
                @"INSERT INTO [cash_app].[dbo].[User](Email, FirstName, LastName, Status, PasswordHash, Registered, LastAccess, PasswordList, 
                PasswordLastChange, LanguageId, CountryId, CurrencyId, UserType)
                  VALUES(@Email, @FirstName, @LastName, @Status, @PasswordHash, @Registered, @LastAccess, @PasswordList, 
                  @PasswordLastChange, @LanguageId, @CountryId, @CurrencyId, @UserType)",
                user);*/

            user.Registered = DateTime.Now;
            user.Status = 1; //activo
            user.UserTypeId = 0; //todo

            _cn.Execute(
                @"INSERT INTO [PaymentDB].[dbo].[User](
                    UserKey, UserTypeId, Status, FirstName, LastName, UserIdentity, Registered, InternalKey
                )
                VALUES(
                    @UserKey, @UserTypeId, @Status, @FirstName, @LastName, @UserIdentity, @Registered, @InternalKey
                )",
                 user
                );

            return 1;
        }

        /*
         * public long GuardarEmpresa(Empresa empresa)
        {
            empresa.Status = 1; //definir enum de status
            var empresaId = _cn.Query<long>(
                @"INSERT INTO [PaymentDB].[dbo].[Empresa](
                    Nombre, 
                    Correo, 
                    Telefono, 
                    Status, 
                    CedulaJuridica, 
                    NombreRepresentanteLegal, 
                    CedulaRepresentanteLegal
                )
                VALUES(
                    @Nombre, 
                    @Correo, 
                    @Telefono, 
                    @Status, 
                    @CedulaJuridica, 
                    @NombreRepresentanteLegal, 
                    @CedulaRepresentanteLegal
                )
                SELECT CAST(SCOPE_IDENTITY() as bigint)",
                 empresa
                ).SingleOrDefault();


            var empresaUsuario = new EmpresaUsuario()
            {
                EmpresaId = empresaId,
                Login = empresa.Correo,
                Clave = empresa.Clave,
                Registered = DateTime.Now,
                Status = 1, //definir enumeracion de status
                Permisos = "A"
            };
            var empresaUsuarioId = _cn.Query<int>(
                @"INSERT INTO [PaymentDB].[dbo].[EmpresaUsuario](
                    EmpresaId, 
                    Login, 
                    Clave, 
                    Registered, 
                    Status, 
                    Permisos
                )
                VALUES(
                    @EmpresaId, 
                    @Login, 
                    @Clave, 
                    @Registered, 
                    @Status, 
                    @Permisos
                )
                SELECT CAST(SCOPE_IDENTITY() as int)",
                 empresaUsuario
                ).SingleOrDefault();

            return 1;
        }


        public List<Ruta> ObtenerRutas(int empresaUsuarioId)
        {
            var strQuery =
                @"SELECT r.RutaId
                    ,r.NombreRuta
                    ,r.EmpresaId
                    ,r.Status
                    ,r.Codigo
                    ,t.TarifaId
                    ,t.Descripcion
                    ,t.Status AS StatusTarifa
                    ,t.Registered AS Registered
                    ,t.Monto
                FROM [PaymentDB].[dbo].[Ruta] AS r
                INNER JOIN [PaymentDB].[dbo].[EmpresaUsuario] AS u
                ON r.EmpresaId = u.EmpresaId
                INNER JOIN [PaymentDB].[dbo].[RutaTarifa] AS t
                ON r.RutaId = t.RutaId
                WHERE u.EmpresaUsuarioId = @empresaUsuarioId";

            var list = _cn.Query<Ruta>(
                    strQuery,
                    new { empresaUsuarioId }
                ).ToList();

            return list;
        }

        public string ActualizarCodigoRuta(long rutaId, string codigo)
        {
            _cn.Execute(
                @"UPDATE [PaymentDB].[dbo].[Ruta]
                    SET Codigo = @codigo
                    WHERE RutaId = @rutaId",
                new { rutaId, codigo });
            return codigo;
        }

        
		public List<Transaccion> ObtenerMovimientos(int fromUserId, DateTime initial_date, DateTime final_date)
        {
            var strQuery =
                @"SELECT t.TransaccionId
	                ,t.CuentaId
	                ,t.Monto
	                ,t.Registered
	                ,t.Status
	                ,t.Offline
	                ,t.CantidadPasajes
	                ,t.Validacion
	                ,t.TipoMovimiento
	                ,c.MontoActual 
	                ,r.NombreRuta
                FROM [PaymentDB].[dbo].[Transaccion] AS t
                INNER JOIN [PaymentDB].[dbo].[Cuenta] AS c
                ON t.CuentaId = c.CuentaId
                INNER JOIN [PaymentDB].[dbo].[Ruta] AS r
                ON c.RutaId = r.RutaId
                INNER JOIN [PaymentDB].[dbo].[EmpresaUsuario] AS u
                ON r.EmpresaId = u.EmpresaId
                WHERE u.EmpresaUsuarioId = @fromUserId
                AND t.Registered BETWEEN @initial_date AND @final_date";

            var list = _cn.Query<Transaccion>(
                    strQuery,
                    new { fromUserId, initial_date, final_date }
                ).ToList();

            return list;
        }
*/
        
		public void Open()
        {
            //Aqui se debe leer la cadena de conexion desde el archivo de configuracion
            _cn.Open(); 
        }

        public void Dispose()
        {
            if (_cn.State == ConnectionState.Open)
                _cn.Close();
        }
    }
}
