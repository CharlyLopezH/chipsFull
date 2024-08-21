using Microsoft.EntityFrameworkCore;
using MnmChipsAPI;
using MnmChipsAPI.Endpoints;
using MnmChipsAPI.Repositorios;
using MnmChipsAPI.Servicios;

var builder = WebApplication.CreateBuilder(args);

var ambiente = builder.Configuration.GetValue<string>("ambiente");

//Variable para la cadena de conexión
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => 
options.UseSqlServer("name=DefaultConnection"));


builder.Services.AddOutputCache();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IRepositorioPrestadores, RepositorioPrestadores>();

builder.Services.AddScoped<IAlmacenadorArchivos,AlmacenadorArchivos>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddAutoMapper(typeof(Program));
//Fin de área de servicios

//Middlewares
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseStaticFiles();

//app.UseCors();

//middlewares
app.UseSwagger();


app.MapGet("/", () => "Hello World! API de prestadores con .Net core "+"(en "+ ambiente+")");
//Endpoints con MapGroup
app.MapGroup("/prestadores").MapPrestadores();


app.Run();
