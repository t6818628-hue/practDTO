using Microsoft.AspNetCore.Mvc;
using p1_api.DTO;
using p1_api.Models;

namespace p1_api.Controllers
{
    [Route("api/cartitem")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        private readonly RpmNewContext _context;

        public CartItemController(RpmNewContext context)
        {
            _context = context;
        }

        private static CartItemDTO ToDto(CartItem item) => new CartItemDTO
        {
            CartItemId = item.CartItemId,
            CartId = item.CartId,
            ProductId = item.ProductId,
            Quantity = item.Quantity
        };

        [HttpGet]
        public IActionResult GetAll(int page = 1, int pageSize = 10)
        {
            var items = _context.CartItems
                .OrderBy(ci => ci.CartItemId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList()
                .Select(ToDto)
                .ToList();

            return Ok(items);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var item = _context.CartItems.FirstOrDefault(ci => ci.CartItemId == id);
            if (item is null) return NotFound();
            return Ok(ToDto(item));
        }

        [HttpPost]
        public IActionResult Create(CreateCartItemDTO dto)
        {
            var item = new CartItem
            {
                CartId = dto.CartId,
                ProductId = dto.ProductId,
                Quantity = dto.Quantity
            };

            _context.CartItems.Add(item);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = item.CartItemId }, ToDto(item));
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateCartItemDTO dto)
        {
            var item = _context.CartItems.FirstOrDefault(ci => ci.CartItemId == id);
            if (item is null) return NotFound();

            item.CartId = dto.CartId;
            item.ProductId = dto.ProductId;
            item.Quantity = dto.Quantity;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, PatchCartItemDTO dto)
        {
            var item = _context.CartItems.FirstOrDefault(ci => ci.CartItemId == id);
            if (item is null) return NotFound();

            if (dto.CartId is not null)
                item.CartId = dto.CartId.Value;

            if (dto.ProductId is not null)
                item.ProductId = dto.ProductId.Value;

            if (dto.Quantity is not null)
                item.Quantity = dto.Quantity.Value;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _context.CartItems.FirstOrDefault(ci => ci.CartItemId == id);
            if (item is null) return NotFound();

            _context.CartItems.Remove(item);
            _context.SaveChanges();
            return NoContent();
        }
    }
}