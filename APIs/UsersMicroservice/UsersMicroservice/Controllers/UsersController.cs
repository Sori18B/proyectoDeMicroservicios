using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

//Referencias
using UsersMicroservice.Entities;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
//using Microsoft.AspNetCore.Components;
using System.Text;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using UsersMicroservice.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace UsersMicroservice.Controllers
{
    [EnableCors("ReglasCors")] //Habilitar los Cors
    [Route("api/[controller]")]
    [Authorize] //Habilitar la autorizacion para consumir la api
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger; // Logger para registrar eventos.
        public readonly IUsers _repository; //Repositorio con la lógica de usuarios
        private readonly IConfiguration _configuration;

        //Crear el contructor
        public UsersController(IConfiguration config, ILogger<UsersController> logger, IUsers repository)
        {
            _logger = logger;
            _repository = repository;
            _configuration = config;
        }

        //Metodos del controlador

        //Método para recuperar una lista de usuarios
        [HttpGet("UsersList")]
        public async Task<IActionResult> UsersList()
        {
            //Bloque Try-Catch
            try
            {
                //Espera la respuesta del repositorio al ejecutar el metodo y sus parametros
                var results = await _repository.UsersList();
                //Regresa una confirmación de la consulta
                return Ok(results);
            }

            catch (Exception ex)
            {
                //Logger para mostrar el error y un mensaje
                _logger.LogError($"No se pudo recuperar la lista de usuarios {ex.Message}");
                //Regresar el codigo del error y un mensaje
                return StatusCode(500, "Internal server error");
            }
        }

        //Metodo para recuperar una lista de usuarios por ID
        [HttpGet("UsersListById/{UserID}")]
        public async Task<IActionResult> UsersListById(int UserID)
        {
            //bloque Try-Catch
            try
            {
                var results = await _repository.UsersListById(UserID);
                return Ok(results);

            }
            catch (Exception ex)
            {
                _logger.LogError($"EEl usuario no se obtuvo {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        //Método para obtener una lista de usuarios por rol
        [HttpGet("UsersListByRole/{RoleID}")]
        public async Task<IActionResult> UsersByRole(int RoleID)
        {
            try
            {
                var results = await _repository.UsersByRole(RoleID);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError($"La lista no se pudo recuperar {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        //Método para obtener una lista de clientes
        [HttpGet("ClientsList")]
        public async Task<IActionResult> ClientsList()
        {
            try
            {
                var results = await _repository.ClientsList();
                return Ok(results);

            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo ercuperar la lista de clientes {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        //Método para obtener una lista de representantes
        [HttpGet("RepresentativesList")]
        public async Task<IActionResult> RepresentativesList()
        {
            try
            {
                var results = await _repository.RepresentativesList();
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo obtener la lista {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("InfoClient/{ClientID}")]
        public async Task<IActionResult> InfoClient(int ClientID)
        {
            try
            {
                var results = await _repository.InfoClient(ClientID);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo obtener la lista {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("InfoRep/{RepID}")]
        public async Task<IActionResult> InfoRep(int RepID)
        {
            try
            {
                var results = await _repository.InfoRep(RepID);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo obtener la lista {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("ClientID/{ClientID}")]
        public async Task<IActionResult> ClientID(int ClientID)
        {
            if (ClientID <= 0)
            {
                return BadRequest("El ClientID debe ser un número positivo.");
            }

            try
            {
                var results = await _repository.ClientID(ClientID);

                if (results == null || !results.Any())
                {
                    return NotFound("No se encontraron tickets para el ClientID proporcionado.");
                }

                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo obtener la lista {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


        //Método para eliminar usuario
        [HttpDelete("DeleteUser/{UserID}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int UserID)
        {
            //Try-Catch
            try
            {
                await _repository.DeleteUser(UserID);
                return Ok("Usuario eliminado exitosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError($"El usuario no se pudo eliminar {ex.Message}", ex);
                return StatusCode(500, "Internal server error");
            }
        }

        //Método para actualizar datos de un usuario
        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserModel model)
        {
            //Bloque Try-Catch

            try
            {
                await _repository.UpdateUser(model.UserID, model.Name, model.Email, model.Password, model.PhoneNumber);
                return Ok("Los datos se actualizaron correctamente");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Los datos no se actualizaron {ex.Message}");
                return StatusCode(500, "Internal server error");

            }
        }


















    }
}
