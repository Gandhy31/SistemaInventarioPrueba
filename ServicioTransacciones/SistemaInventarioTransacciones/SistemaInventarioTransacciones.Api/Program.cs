using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaInventarios.Dominio.Entidades;
using SistemaInventarios.Dominio.Interfaces;
using SistemaInventarios.Infraestructura.Contextos;
using SistemaInventarioTransacciones.Api.Mappings;
using SistemaInventarioTransacciones.Aplicacion.Interfaces;
using SistemaInventarioTransacciones.Aplicacion.Servicios;
using SistemaInventarioTransacciones.Infraestructura.Repositorios;
using SistemaInventarioTransacciones.Infraestructura.Servicios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddPolicy("NuevaPolitica", app =>
    {
        app.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

string connSqlServer = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<SistemaInventariosContext>
    (
        dbContextOptions => dbContextOptions
            .UseSqlServer(connSqlServer)
    );
// AutoMapper
var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new PerfilMapping());
});

IMapper mapper = mappingConfig.CreateMapper();

builder.Services.AddHttpClient<IProductoService, ProductoService>((serviceProvider, client) =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    var baseUrl = builder.Configuration["ServiciosExternos:Producto:BaseUrl"];
    client.BaseAddress = new Uri(baseUrl!);
});

//builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<ITransaccionService, TransaccionService>();
builder.Services.AddScoped<ITransaccionRepository, TransaccionRepository>();

builder.Services.AddSingleton(mapper);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("NuevaPolitica");

app.UseAuthorization();

app.MapControllers();

app.Run();
