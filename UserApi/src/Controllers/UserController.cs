using Microsoft.AspNetCore.Mvc;
using UserApi.Models;
using UserApi.Repositories;

namespace UserApi.Controllers
{
    // Controlador para manejar los endpoints relacionados con la gestión de usuarios.
    [ApiController]
    [Route("[controller]")]
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
            if (string.IsNullOrWhiteSpace(user.Name) || user.Name.Length < 3 || user.Name.Length > 20)
                return BadRequest("Invalid name.");

            if (string.IsNullOrWhiteSpace(user.Email) || !user.Email.Contains("@"))
                return BadRequest("Invalid email.");

            if (user.BirthDate >= DateTime.Now)
                return BadRequest("Invalid birth date.");

            if (!new[] { "Femenino", "Masculino", "Prefiero no decirlo", "Otro" }.Contains(user.Gender))
                return BadRequest("Invalid gender.");

            await _repository.AddAsync(user);
            return Created("", user);
        }
    }
}
