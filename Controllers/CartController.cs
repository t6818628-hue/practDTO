using Microsoft.AspNetCore.Mvc;
using p1_api.DTO;
using p1_api.Models;

namespace p1_api.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly RpmNewContext _context;

        public CartController(RpmNewContext context)
        {
            _context = context;
        }

        private static CartDTO ToDto(Cart cart) => new CartDTO
        {
            CartId = cart.CartId,
            UserId = cart.UserId,
            CreatedDate = cart.CreatedDate
        };

        [HttpGet]
        public IActionResult GetAll(int page = 1, int pageSize = 10)
        {
            var carts = _context.Carts
                .OrderBy(c => c.CartId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList()
                .Select(ToDto)
                .ToList();

            return Ok(carts);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var cart = _context.Carts.FirstOrDefault(c => c.CartId == id);
            if (cart is null) return NotFound();
            return Ok(ToDto(cart));
        }

        [HttpPost]
        public IActionResult Create(CreateCartDTO dto)
        {
            var cart = new Cart
            {
                UserId = dto.UserId,
                CreatedDate = DateTime.UtcNow
            };

            _context.Carts.Add(cart);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = cart.CartId }, ToDto(cart));
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateCartDTO dto)
        {
            var cart = _context.Carts.FirstOrDefault(c => c.CartId == id);
            if (cart is null) return NotFound();

            cart.UserId = dto.UserId;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, PatchCartDTO dto)
        {
            var cart = _context.Carts.FirstOrDefault(c => c.CartId == id);
            if (cart is null) return NotFound();

            if (dto.UserId is not null)
                cart.UserId = dto.UserId.Value;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var cart = _context.Carts.FirstOrDefault(c => c.CartId == id);
            if (cart is null) return NotFound();

            _context.Carts.Remove(cart);
            _context.SaveChanges();
            return NoContent();
        }
    }
}