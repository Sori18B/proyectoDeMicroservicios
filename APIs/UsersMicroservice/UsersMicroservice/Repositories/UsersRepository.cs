using Dapper;
using System.Data.SqlClient;
using System.Data;
using UsersMicroservice.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace UsersMicroservice.Repositories
{
    public class UsersRepository : IUsers
    {
        //--------------------------

        //Definir una variable privada y de solo lectura para la cadena de conexión a la bd

        private readonly string conexionString;

        //Definir el constructor que recibe la configuración de la aplicación y obtiene la cadena de conexión.

        public UsersRepository(IConfiguration configuration)
        {
            conexionString = configuration.GetConnectionString("CadenaSQL");
        }

        //Implementacion de los metodos

        //--------------------------Método para logear al usuario, menu y acciones
        public async Task<LoginResponseModel> UserLogIn(LoginModel model)
        {
            //Establecer la conexión
            using var connection = new SqlConnection(conexionString);

            //Llamar al procedimiento almacenado
            var procedure = "UserLogIn";

            //Agregar parámetros
            var parameters = new DynamicParameters();
            parameters.Add("@Email", model.Email);

            //Try-Catch
            try
            {
                var user = await connection.QueryFirstOrDefaultAsync<LoginResponseModel>(procedure, parameters, commandType: CommandType.StoredProcedure);
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en las credenciales del usuario {ex.Message}");
                throw;
            }
        }

        public async Task<string> GetPasswordHash(string Email)
        {
            using var connection = new SqlConnection(conexionString);
            var procedure = "PasswordHash";

            var parameters = new DynamicParameters();
            parameters.Add("@Email", Email);

            //Try-Catch
            try
            {
                var pass = await connection.QueryFirstOrDefaultAsync<string>(procedure, parameters, commandType: CommandType.StoredProcedure);
                return pass;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en las credenciales del usuario {ex.Message}");
                throw;
            }
        }


        public List<Menu> MenuDinamico(int RoleID)
        {
            List<Menu> menus = new(); // Lista de menús que será devuelta.

            // Se definen los menús según el rol del usuario.
            switch (RoleID)
            {
                case 1:
                    menus.Add(new Menu() { Titulo = "Inicio",                       Icono = "Inicio",           Url = "/Inicio" });
                    menus.Add(new Menu() { Titulo = "Tickets pendientes",           Icono = "Pendientes",       Url = "/TicketsPendientes" }); //los preliminares
                    menus.Add(new Menu() { Titulo = "Completar tickets",            Icono = "Tickets",          Url = "/CompletarTicket" }); //completar ticket
                    menus.Add(new Menu() { Titulo = "Lista de tickets",             Icono = "VerTickets",       Url = "/ListaTickets" });
                    menus.Add(new Menu() { Titulo = "Actualizar ticket",            Icono = "Actualizar",       Url = "/ActualizarTicket" });
                    menus.Add(new Menu() { Titulo = "Usuarios",                     Icono = "Users",            Url = "PENDIENTE" });
                    menus.Add(new Menu() { Titulo = "Configuración",                Icono = "Configuracion",    Url = "/Configurar_Perfil" });
                    menus.Add(new Menu() { Titulo = "Cerrar sesión",                Icono = "Salir",            Url = "" });
                    break;
                case 2:
                    menus.Add(new Menu() { Titulo = "Inicio",                       Icono = "Inicio",           Url = "/Inicio" });
                    menus.Add(new Menu() { Titulo = "Tickets pendientes",           Icono = "Pendientes",       Url = "/TicketsPendientes" }); //los preliminares
                    menus.Add(new Menu() { Titulo = "Lista de tickets",             Icono = "VerTickets",       Url = "/ListaTickets" });
                    menus.Add(new Menu() { Titulo = "Configuración",                Icono = "Configuracion",    Url = "/Configurar_Perfil" });
                    menus.Add(new Menu() { Titulo = "Cerrar sesión",                Icono = "Salir",            Url = "" });
                    break;
                case 3:
                    menus.Add(new Menu() { Titulo = "Inicio",           Icono = "Inicio", Url = "/Inicio" });
                    menus.Add(new Menu() { Titulo = "Crear ticket",     Icono = "Tickets", Url = "/TicketPreliminar" }); //CREAR los preliminares
                    menus.Add(new Menu() { Titulo = "Mis tickets",      Icono = "VerTickets", Url = "/Mistickets" }); // 'Mis tickets'
                    menus.Add(new Menu() { Titulo = "Configuración",    Icono = "Configuracion", Url = "/Configurar_Perfil" });
                    menus.Add(new Menu() { Titulo = "Cerrar sesión",    Icono = "Salir", Url = "" });
                    break;
            }
            return menus; // Retorna la lista de menús según el rol.
        }

        //--------------------------Método para logear al usuario, menu y acciones



        //--------------------------Método para recuperar una lista de usuarios
        public async Task<IEnumerable<UsersList>> UsersList()
        {
            //Establecer la conexion
            using var connection = new SqlConnection(conexionString);
            ;
            //Llamar al procedimiento almacenado
            var procedure = "UsersList";

            //Agregar los parámetros
            var parameters = new DynamicParameters();

            //Try-Catch
            try
            {
                var usersList = await connection.QueryAsync<UsersList>(procedure, parameters, commandType: CommandType.StoredProcedure);
                return usersList;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"No se pudo recuperar la lista de usuarios {ex.Message} ");
                throw;
            }
        }
        //--------------------------Método para recuperar una lista de usuarios



        //--------------------------Metodo para recuperar una lista de usuarios por ID
        public async Task<UsersList> UsersListById(int UserID)
        {
            //Establecer la conexión
            using var connection = new SqlConnection(conexionString);

            //Llamar al procedimiento
            var procedure = "UsersListById";

            //Agregar parámetros
            var parameters = new DynamicParameters();
            parameters.Add("@UserID", UserID);

            //Try-Catch
            try
            {
                var user = await connection.QueryFirstOrDefaultAsync<UsersList>(procedure, parameters, commandType: CommandType.StoredProcedure);
                return user;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al recuperar al usuario {ex.Message}");
                throw;
            }
        }
        //--------------------------Metodo para recuperar una lista de usuarios por ID



        //--------------------------Método para obtener una lista de usuarios por rol
        public async Task<IEnumerable<UsersList>> UsersByRole(int RoleID)
        {
            //Establecer la conexión
            using var connection = new SqlConnection(conexionString);

            //Llamar al procedimiento almacenado

            var procedure = "UsersByRole";

            //Agregar parámetros
            var parameters = new DynamicParameters();
            parameters.Add("@RoleID", RoleID);

            //Try-Catch
            try
            {
                var users = await connection.QueryAsync<UsersList>(procedure, parameters, commandType: CommandType.StoredProcedure);
                return users;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener la lista de usuarios {ex.Message}");
                throw;
            }
        }
        //--------------------------Método para obtener una lista de usuarios por rol



        //--------------------------Método para obtener una lista de clientes
        public async Task<IEnumerable<ClientsList>> ClientsList()
        {
            //Establecer la conexión
            using var connection = new SqlConnection(conexionString);

            //Llamar al procedimiento almacenado
            var procedure = "ClientsList";

            //Agregar parámetros
            var parameters = new DynamicParameters();

            //Try-Catch
            try
            {
                var clients = await connection.QueryAsync<ClientsList>(procedure, parameters, commandType: CommandType.StoredProcedure);
                return clients;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"No se pudo obtener la lista de clientes {ex.Message}");
                throw;
            }
        }
        //--------------------------Método para obtener una lista de clientes



        //--------------------------Método para obtener una lista de representantes
        public async Task<IEnumerable<RepresentativesList>> RepresentativesList()
        {
            //Establecer la conexión
            using var connection = new SqlConnection(conexionString);

            //Llamar al procedimiento almacenado
            var procedure = "RepresentativesList";

            //Agregar parámetros
            var parameters = new DynamicParameters();

            //Try-Catch
            try
            {
                var reps = await connection.QueryAsync<RepresentativesList>(procedure, parameters, commandType: CommandType.StoredProcedure);
                return reps;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"No se pudo obtener la lista de representantes {ex.Message}");
                throw;
            }
        }
        //--------------------------Método para obtener una lista de representantes

        //--------------------------Método para obtener info del cliente
        public async Task<UserInfoModel> InfoClient(int ClientID)
        {
            using var connection = new SqlConnection(conexionString);
            var procedure = "InfoClient";
            var parameters = new DynamicParameters();
            parameters.Add("@ClientID", ClientID);

            //Try-Catch
            try
            {
                var client = await connection.QueryFirstOrDefaultAsync<UserInfoModel>(procedure, parameters, commandType: CommandType.StoredProcedure);
                return client;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"No se pudo obtener la info {ex.Message}");
                throw;
            }
        }

        //--------------------------Método para obtener info del cliente

        //--------------------------Método para obtener info del representante
        public async Task<UserInfoModel> InfoRep(int RepID)
        {
            using var connection = new SqlConnection(conexionString);
            var procedure = "InfoRep";
            var parameters = new DynamicParameters();
            parameters.Add("@RepID", RepID);

            //Try-Catch
            try
            {
                var rep = await connection.QueryFirstOrDefaultAsync<UserInfoModel>(procedure, parameters, commandType: CommandType.StoredProcedure);
                return rep;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"No se pudo obtener la info {ex.Message}");
                throw;
            }
        }
        //--------------------------Método para obtener info del representante


        public async Task<IEnumerable<ClientIDModel>> ClientID(int ClientID)
        {
            using var connection = new SqlConnection(conexionString);
            var procedure = "TicketListByClientID";
            var parameters = new DynamicParameters();
            parameters.Add("@ClientID", ClientID);

            //Try-Catch
            try
            {
                var rep = await connection.QueryAsync<ClientIDModel>(procedure, parameters, commandType: CommandType.StoredProcedure);
                return rep;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"No se pudo obtener la info {ex.Message}");
                throw;
            }
        }


        //Metodo para registrar usuario
        public async Task RegisterUser(RegisterUserModel model)
        {
            //Usar una variable para establecer la conexión a la bd usando la cadena de conexión
            using var connection = new SqlConnection(conexionString);

            //Usar una variable para almacenar el nombre del procedimiento almacenado
            var procedure = "RegisterUser";

            //Establecer los parametros que recibirá el procedimiento utilizando DynamicParameters
            var parameters = new DynamicParameters();
            parameters.Add("@Name", model.Name); // "parametroProcAlm", parametroDelMetodo
            parameters.Add("@Email", model.Email);
            parameters.Add("@Password", model.Password);
            parameters.Add("@PhoneNumber", model.PhoneNumber);
            parameters.Add("@RoleID", model.RoleID);
            // Añade este log para ver los parámetros recibidos en el método del repositorio
            Console.WriteLine($"Parámetros: Nombre={model.Name}, Email={model.Email}, Password={model.Password}, PhoneNumber={model.PhoneNumber}, RoleID={model.RoleID}");

            try
            {
                //Ejecutar el procedimiento almacenado
                await connection.ExecuteAsync(procedure, parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al registrar usuario: {ex.Message}");
                throw;
            }
        }

        //Metodo para eliminar usuario
        public async Task DeleteUser([FromBody]int UserID)
        {
            //Establecer la conexion
            using var connection = new SqlConnection(conexionString);

            //Llamar al procedimiento almacenado
            var procedure = "DeleteUser";

            //Agregar parametros
            var parameters = new DynamicParameters();
            parameters.Add("@UserID", UserID);

            //Try-Catch
            try
            {
                await connection.ExecuteAsync(procedure, parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar usuario: {ex.Message}");
                throw;
            }
        }

        //Método para actualizar datos del usuario
        public async Task UpdateUser(int UserID, string Name, string Email, string Password, string PhoneNumber)
        {
            //Establecer la conexión
            using var connection = new SqlConnection(conexionString);

            //Llamamos al procedimiento almacenado
            var procedure = "UpdateUser";

            //Agregamos los parametros
            var parameters = new DynamicParameters();
            parameters.Add("@UserID", UserID);
            parameters.Add("@Name", Name);
            parameters.Add("@Email", Email);
            parameters.Add("@Password", Password);
            parameters.Add("@PhoneNumber", PhoneNumber);

            //Try-Catch
            try
            {
                await connection.ExecuteAsync(procedure, parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar los datos del usuario {ex.Message}");
                throw;
            }
        }

    }
}
