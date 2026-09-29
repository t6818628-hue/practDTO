using Microsoft.AspNetCore.Mvc;
using p1_api.DTO;
using p1_api.Models;

namespace p1_api.Controllers
{
    [Route("api/role")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RpmNewContext _context;

        public RoleController(RpmNewContext context)
        {
            _context = context;
        }

        private static RoleDTO ToDto(Role role) => new RoleDTO
        {
            RoleId = role.RoleId,
            RoleName = role.RoleName
        };

        [HttpGet]
        public IActionResult GetAll(int page = 1, int pageSize = 10)
        {
            var roles = _context.Roles
                .OrderBy(r => r.RoleId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList()
                .Select(ToDto)
                .ToList();

            return Ok(roles);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var role = _context.Roles.FirstOrDefault(r => r.RoleId == id);
            if (role is null) return NotFound();
            return Ok(ToDto(role));
        }

        [HttpPost]
        public IActionResult Create(CreateRoleDTO dto)
        {
            var role = new Role
            {
                RoleName = dto.RoleName
            };

            _context.Roles.Add(role);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = role.RoleId }, ToDto(role));
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateRoleDTO dto)
        {
            var role = _context.Roles.FirstOrDefault(r => r.RoleId == id);
            if (role is null) return NotFound();

            role.RoleName = dto.RoleName;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, PatchRoleDTO dto)
        {
            var role = _context.Roles.FirstOrDefault(r => r.RoleId == id);
            if (role is null) return NotFound();

            if (dto.RoleName is not null)
                role.RoleName = dto.RoleName;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var role = _context.Roles.FirstOrDefault(r => r.RoleId == id);
            if (role is null) return NotFound();

            _context.Roles.Remove(role);
            _context.SaveChanges();
            return NoContent();
        }
    }
}