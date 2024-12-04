using UserApi.Models;
using Microsoft.EntityFrameworkCore;

namespace UserApi.Data
{
    // Clase para inicializar datos de prueba en la base de datos.
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new AppDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>());

            // Verifica si ya existen datos en la tabla Users
            if (context.Users.Any())
            {
                return;
            }

            // Agregar usuarios iniciales
            context.Users.AddRange(
                new User
                {
                    Name = "Juan Pérez",
                    Email = "juan.perez@example.com",
                    BirthDate = new DateTime(1990, 1, 1),
                    Gender = "Masculino"
                },
                new User
                {
                    Name = "María López",
                    Email = "maria.lopez@example.com",
                    BirthDate = new DateTime(1985, 5, 15),
                    Gender = "Femenino"
                },
                new User
                {
                    Name = "Alex García",
                    Email = "alex.garcia@example.com",
                    BirthDate = new DateTime(2000, 3, 10),
                    Gender = "Prefiero no decirlo"
                }
            );
            
            context.SaveChanges();
        }
    }
}
