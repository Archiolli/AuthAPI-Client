using AppClients;
using AppClients.Data;
using AppClients.Controllers;
using AppClients.Repositories;
using AppClients.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var key = Encoding.ASCII.GetBytes(Settings.Secret);

// Configurou a autentica??o na aplica??o
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

//Configurou a autoriza??o
builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    x.AddPolicy("Colaborador", policy => policy.RequireRole("Colaborador"));
});

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddEndpointsApiExplorer();



var app = builder.Build();

app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();


app.MapGet("/saldo", (ClaimsPrincipal user) =>
{
    var mensagem = "Seu saldo ? R$ 1.000.000.000,00";

    return Results.Ok(mensagem);
}).RequireAuthorization("Admin");


app.Run();
