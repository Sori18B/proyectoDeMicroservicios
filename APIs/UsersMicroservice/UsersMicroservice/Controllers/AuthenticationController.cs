using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UsersMicroservice.Entities;
using UsersMicroservice.Repositories;

namespace UsersMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly string secretkey; //Obtener la llave secreta para el uso del JWT
        private readonly ILogger<UsersController> _logger; // Logger para registrar eventos.
        public readonly IUsers _repository; //Repositorio con la lógica de usuarios
        private readonly IConfiguration _configuration;

        //Crear el contructor
        public AuthenticationController(IConfiguration config, ILogger<UsersController> logger, IUsers repository)
        {
            //Obtener la secretkey del json
            secretkey = config.GetValue<string>("settings:secretkey");
            //_logger = logger;
            _repository = repository;
            _configuration = config;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> UserLogIn([FromBody] LoginModel request)
        {
            if (request == null || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("El correo y la contraseña son necesarios");
            }

            var hashedPassword = await _repository.GetPasswordHash(request.Email);
            // Agregar logs detallados
            Console.WriteLine($"Hash obtenido de la BD: {hashedPassword}");
            Console.WriteLine($"Contraseña ingresada: {request.Password}");

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, hashedPassword);
            Console.WriteLine($"¿Contraseña válida?: {isPasswordValid}");

            var user = await _repository.UserLogIn(request);
            if (user == null)
            {
                return Unauthorized("Correo o contraseña inválidos");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("SEXPULVEDARIATONDICKGIAKUMECOSXD");

            var claims = new List<Claim>
            {
                new Claim("UserID", user.UserID.ToString()),
                new Claim("RoleID", user.RoleID.ToString()),
                new Claim("Email", user.Email)
            };

            var menu = _repository.MenuDinamico(user.RoleID);
            var menuJson = JsonConvert.SerializeObject(menu);
            claims.Add(new Claim("menu", menuJson));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { token = tokenString });
        }



        //Método para registrar un nuevo usuario
        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserModel model)
        {
            Console.WriteLine($"Datos recibidos: {System.Text.Json.JsonSerializer.Serialize(model)}"); // Log para validar datos
            try
            {
                //string HashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);
                //await _repository.RegisterUser(model);
                model.Password = BCrypt.Net.BCrypt.HashPassword(model.Password); // Sobrescribe la contraseña con el hash
                await _repository.RegisterUser(model);
                return Ok(new { Message = "Usuario registrado exitosamente" });

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en la actualización {ex.Message}");
                return StatusCode(500, "Internal server error");

            }
        }
    }
}
