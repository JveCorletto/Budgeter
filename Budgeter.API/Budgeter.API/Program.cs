using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Budgeter.API.dbContext;
using Budgeter.API.Services.UsuariosService;
using Budgeter.API.Services.MetodosPagoService;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();

//Configuración de JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["JWT:JWT_ISSUER_TOKEN"],
            ValidAudience = builder.Configuration["JWT:JWT_AUDIENCE_TOKEM"],

            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:JWT_SECRET_KEY"]))
        };
    });

//Se agrega el contexto de la base de datos
builder.Services.AddDbContext<DataContext>();

// Se agrega la inyección de dependencias de los servicios
// Usuarios
builder.Services.AddScoped<iUsuariosService, UsuariosService>();

// Catálogos
builder.Services.AddScoped<iMetodosPagosService, MetodosPagosService>();

builder.Services.AddControllers();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();