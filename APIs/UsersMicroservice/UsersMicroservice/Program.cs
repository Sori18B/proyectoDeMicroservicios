using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UsersMicroservice.Repositories;

var builder = WebApplication.CreateBuilder(args);


//Configuración de los Cors
    var misReglasCors = "ReglasCors";

    builder.Services.AddCors(option =>  //Agregar una nueva políitica
        option.AddPolicy(name: misReglasCors,
            builder =>
            {
                builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                //permitir cualquier Origen, Cabecera y Método
            }
        ) 
     );
//Configuración de los Cors



// Add services to the container.

    //Configuración del JWT

        //Obtener el archivo appsettings.json
        builder.Configuration.AddJsonFile("appsettings.json");
        //Obtener el secretkey del archivo json y convertirlo a string
        //var secretkey = builder.Configuration.GetValue<string>("settings:secretkey");
        //Convertir la llave secreta en  bytes
        //var keyBytes = Encoding.UTF8.GetBytes(secretkey);

        builder.Services.AddAuthentication(config =>
        {
            config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

        }).AddJwtBearer(config => {
            config.RequireHttpsMetadata = false; //Si no se usa https se pone falso
            config.SaveToken = true; //Para guardar el token
            config.TokenValidationParameters = new TokenValidationParameters //Validar los parámetros
            {
                //Validación cada vez que el usuario se logea, el usuario accede a la información solo con las credenciales
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SEXPULVEDARIATONDICKGIAKUMECOSXD")), 
                ValidateIssuer = false,
                ValidateAudience = false,
            };

            config.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    Console.WriteLine("Error al autenticar el token: " + context.Exception.ToString());
                    return Task.CompletedTask;
                }
            };
        });

    //Configuración del JWT

    // Por defecto
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    //Inyección de dependencias (de el repositorio utilizado)
    builder.Services.AddScoped<IUsers, UsersRepository>();

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Agregada para los Cors
app.UseCors(misReglasCors);

//Agregada para el JWT
app.UseAuthentication();

//Por defecto
app.UseAuthorization();
app.MapControllers();
app.Run();
