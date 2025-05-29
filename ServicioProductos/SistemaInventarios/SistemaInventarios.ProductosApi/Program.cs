using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaInventarios.Aplicacion.Interfaces;
using SistemaInventarios.Aplicacion.Services;
using SistemaInventarios.Dominio.Interfaces;
using SistemaInventarios.Infraestructura.Contextos;
using SistemaInventarios.Infraestructura.Repositorios;
using SistemaInventarios.ProductosApi.Mappings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

string connSqlServer = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<SistemaInventariosContext>
    (
        dbContextOptions => dbContextOptions
            .UseSqlServer(connSqlServer)
    );


builder.Services.AddCors(options =>
{
    options.AddPolicy("NuevaPolitica", app =>
    {
        app.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});
// AutoMapper
var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new PerfilMappings());
});

IMapper mapper = mappingConfig.CreateMapper();

// Servicios
builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
//builder.Services.AddScoped(typeof(DominioGenerico<>));

builder.Services.AddSingleton(mapper);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
   // app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("NuevaPolitica");

app.UseAuthorization();

app.MapControllers();

app.Run();
