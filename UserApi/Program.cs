using Microsoft.EntityFrameworkCore;
using UserApi.Data;
using UserApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configuración de servicios
builder.Services.AddControllers(); // Agregar soporte para controladores

// Configuración del DbContext con SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registro del repositorio como servicio para inyección de dependencias
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Configuración de Swagger para documentación de la API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader() 
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Aplicar migraciones automáticamente al iniciar la aplicación
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        context.Database.Migrate();
        SeedData.Initialize(services);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error al aplicar las migraciones: {ex.Message}");
    }
}

// Configuración del pipeline de solicitudes HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Activar CORS
app.UseCors("AllowAll");

// Forzar HTTPS en las solicitudes
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Inicia la aplicación
app.Run();
