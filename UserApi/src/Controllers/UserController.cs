using Microsoft.AspNetCore.Mvc;
using UserApi.Models;
using UserApi.Repositories;

namespace UserApi.Controllers
{
    // Controlador para manejar los endpoints relacionados con la gestión de usuarios.
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }

        // Obtiene todos los usuarios registrados en el sistema.
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _repository.GetAllAsync();
            return Ok(users);
        }

        // Crea un nuevo usuario en el sistema.
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] User user)
        {
            // Validar el nombre
            if (string.IsNullOrWhiteSpace(user.Name) || user.Name.Length < 3 || user.Name.Length > 20)
                return BadRequest("El nombre debe tener entre 3 y 20 caracteres.");

            // Validar el correo electrónico
            if (string.IsNullOrWhiteSpace(user.Email) || !user.Email.Contains("@"))
                return BadRequest("El correo electrónico debe ser válido.");

            // Verificar si el correo ya existe
            var existingUsers = await _repository.GetAllAsync();
            if (existingUsers.Any(u => u.Email.Equals(user.Email, StringComparison.OrdinalIgnoreCase)))
                return BadRequest("El correo electrónico ya está registrado.");

            // Validar la fecha de nacimiento
            if (user.BirthDate >= DateTime.Now)
                return BadRequest("La fecha de nacimiento debe ser anterior a la fecha actual.");

            // Validar el género
            if (!new[] { "Femenino", "Masculino", "Prefiero no decirlo", "Otro" }.Contains(user.Gender))
                return BadRequest("El género debe ser uno de los valores permitidos.");

            // Agregar el usuario a la base de datos
            await _repository.AddAsync(user);
            return Created("", user);
        }
    }
}
