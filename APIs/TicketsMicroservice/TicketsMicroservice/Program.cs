using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TicketsMicroservice;
using TicketsMicroservice.Repositories;

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

    //JWT
    builder.Services.AddAuthentication(config =>
    {
        config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

    }).AddJwtBearer(config => {
        config.TokenValidationParameters = new TokenValidationParameters //Validar los parámetros
        {
            //Validación cada vez que el usuario se logea, el usuario accede a la información solo con las credenciales
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SEXPULVEDARIATONDICKGIAKUMECOSXD")),
            ValidateIssuer = false,
            ValidateAudience = false,
            //ClockSkew = TimeSpan.Zero
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
    //JWT

    // Agrega esta línea para configurar HttpClient en DI
    builder.Services.AddHttpClient();

    //Por defecto
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    //inyeccion de dependencias del repositorio
    builder.Services.AddScoped<ITickets, TicketsRepository>();


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

//Agregada para el JwtMiddleware
app.UseMiddleware<JwtMiddleware>();

//Agregada para el JWT
app.UseAuthentication();

//Por defecto
app.UseAuthorization();
app.MapControllers();
app.Run();
