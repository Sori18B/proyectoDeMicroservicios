using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TicketsMicroservice;
using TicketsMicroservice.Repositories;

var builder = WebApplication.CreateBuilder(args);


//Configuraci�n de los Cors
var misReglasCors = "ReglasCors";

builder.Services.AddCors(option =>  //Agregar una nueva pol�itica
    option.AddPolicy(name: misReglasCors,
        builder =>
        {
            builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            //permitir cualquier Origen, Cabecera y M�todo
        }
    )
 );
//Configuraci�n de los Cors


// Add services to the container.

    //JWT
    builder.Services.AddAuthentication(config =>
    {
        config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

    }).AddJwtBearer(config => {
        config.TokenValidationParameters = new TokenValidationParameters //Validar los par�metros
        {
            //Validaci�n cada vez que el usuario se logea, el usuario accede a la informaci�n solo con las credenciales
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

    // Agrega esta l�nea para configurar HttpClient en DI
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
