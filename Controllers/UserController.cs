using Microsoft.AspNetCore.Mvc;
using p1_api.DTO;
using p1_api.Models;

namespace p1_api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly RpmNewContext _context;

        public UserController(RpmNewContext context)
        {
            _context = context;
        }

        private static UserDTO ToDto(User user) => new UserDTO
        {
            UserId = user.UserId,
            Login = user.Login,
            PasswordHash = user.PasswordHash,
            RoleId = user.RoleId,
            IsActive = user.IsActive
        };

        [HttpGet]
        public IActionResult GetAll(int page = 1, int pageSize = 10)
        {
            var users = _context.Users
                .OrderBy(u => u.UserId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList()
                .Select(ToDto)
                .ToList();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == id);
            if (user is null) return NotFound();
            return Ok(ToDto(user));
        }

        [HttpPost]
        public IActionResult Create(CreateUserDTO dto)
        {
            var user = new User
            {
                Login = dto.Login,
                PasswordHash = dto.PasswordHash,
                RoleId = dto.RoleId,
                IsActive = dto.IsActive
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = user.UserId }, ToDto(user));
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateUserDTO dto)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == id);
            if (user is null) return NotFound();

            user.Login = dto.Login;
            user.PasswordHash = dto.PasswordHash;
            user.RoleId = dto.RoleId;
            user.IsActive = dto.IsActive;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, PatchUserDTO dto)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == id);
            if (user is null) return NotFound();

            if (dto.Login is not null)
                user.Login = dto.Login;

            if (dto.PasswordHash is not null)
                user.PasswordHash = dto.PasswordHash;

            if (dto.RoleId is not null)
                user.RoleId = dto.RoleId.Value;

            if (dto.IsActive is not null)
                user.IsActive = dto.IsActive.Value;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == id);
            if (user is null) return NotFound();

            _context.Users.Remove(user);
            _context.SaveChanges();
            return NoContent();
        }
    }
}