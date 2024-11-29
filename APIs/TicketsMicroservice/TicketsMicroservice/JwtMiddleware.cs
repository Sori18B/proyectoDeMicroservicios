using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace TicketsMicroservice
{
    public class JwtMiddleware
    {
        //Variables y constructor
        private readonly RequestDelegate _next; //Representa el siguiente middleware que se ejecutará después de este.
        private readonly string _secretkey; //Almacenar la clave secreta para validar el token

        //Se define el constructor y se inicializan las variables
        public JwtMiddleware(IConfiguration config, RequestDelegate next)
        {
            _next = next;
            _secretkey = config.GetValue<string>("settings:secretkey");
        }

        //Método para las solicitudes que llegan API
        public async Task InvokeAsync(HttpContext context)
        {
            //extracción del token de la solicitud
            var token = context.Request.Headers["Authorization"] //Accede a los encabezados de la sol. y busca el encabezado de autorización
                .FirstOrDefault()? //Toma el primer valor del encabezado Authorization, si existe.
                .Split(" ") //Divide el valor del encabezado Authorization en partes, usando el espacio como delimitador
                .Last(); //Toma la última parte después de dividir, que es el token en sí.

            //validar el token obtenido de la solicitud
            if (token != null) //Verificamos que se encontro un token en el encabezado Authorization
            {
                context.Items["JWTToken"] = token; // Almacena el token completo en context.Items
                ValidateToken(context, token);
            }
            await _next(context); // Llama al siguiente middleware en la cadena
        }

        //Método para validar el token

        private void ValidateToken(HttpContext context, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler(); //Clase del paquete NuGet que proporciona métodos para validar y manejar tokens JWT.
                var key = Encoding.UTF8.GetBytes(_secretkey); //Convierte la clave secreta (string) a un array de bytes, que es lo que se necesita para firmar o validar.

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true, //Verifica la firma del token con la clave secreta.
                    IssuerSigningKey = new SymmetricSecurityKey(key), //La clave usada para validar la firma del token.
                    ValidateIssuer = false, //Opcionales
                    ValidateAudience = false, //Opcionales
                    ClockSkew = TimeSpan.Zero //Define una tolerancia de tiempo para la expiración del token
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                // Itera sobre todos los claims del token y los agrega al contexto
                foreach (var claim in jwtToken.Claims)
                {
                    context.Items[claim.Type] = claim.Value; // Almacena cada claim en context.Items
                }
            }

            catch
            {
                // Si hay un error en la validación, no hacemos nada; el endpoint puede manejar la respuesta
            }
        }
    }
}
