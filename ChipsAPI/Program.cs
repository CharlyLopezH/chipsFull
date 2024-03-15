using ChipsAPI.Context;
using Microsoft.EntityFrameworkCore;
using NuGet.Frameworks;
using System.Configuration;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Crear variable para la cadena de conexión
var connectionString = builder.Configuration.GetConnectionString("Connection");

//registrar servicio para la conexion
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlServer(connectionString)
);

builder.Services.AddAutoMapper(typeof(Program)); 

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options=>
{

    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:5173")
               .AllowAnyHeader()
               .AllowAnyMethod()
               .WithExposedHeaders(new string[] { "cantidadTotalRegistros" });

    });
});

builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //Con esta línea activada podemos colocar nuestros propios mensajes y contenidos en las respuestas de los EP
    app.UseDeveloperExceptionPage();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors();

app.MapControllers();


app.Run();
