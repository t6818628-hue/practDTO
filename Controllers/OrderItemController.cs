using Microsoft.AspNetCore.Mvc;
using p1_api.DTO;
using p1_api.Models;

namespace p1_api.Controllers
{
    [Route("api/orderitem")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly RpmNewContext _context;

        public OrderItemController(RpmNewContext context)
        {
            _context = context;
        }

        private static OrderItemDTO ToDto(OrderItem item) => new OrderItemDTO
        {
            OrderItemId = item.OrderItemId,
            OrderId = item.OrderId,
            ProductId = item.ProductId,
            Quantity = item.Quantity,
            PriceAtTime = item.PriceAtTime
        };

        [HttpGet]
        public IActionResult GetAll(int page = 1, int pageSize = 10)
        {
            var items = _context.OrderItems
                .OrderBy(oi => oi.OrderItemId)
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
            var item = _context.OrderItems.FirstOrDefault(oi => oi.OrderItemId == id);
            if (item is null) return NotFound();
            return Ok(ToDto(item));
        }

        [HttpPost]
        public IActionResult Create(CreateOrderItemDTO dto)
        {
            var item = new OrderItem
            {
                OrderId = dto.OrderId,
                ProductId = dto.ProductId,
                Quantity = dto.Quantity,
                PriceAtTime = dto.PriceAtTime
            };

            _context.OrderItems.Add(item);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = item.OrderItemId }, ToDto(item));
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateOrderItemDTO dto)
        {
            var item = _context.OrderItems.FirstOrDefault(oi => oi.OrderItemId == id);
            if (item is null) return NotFound();

            item.OrderId = dto.OrderId;
            item.ProductId = dto.ProductId;
            item.Quantity = dto.Quantity;
            item.PriceAtTime = dto.PriceAtTime;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, PatchOrderItemDTO dto)
        {
            var item = _context.OrderItems.FirstOrDefault(oi => oi.OrderItemId == id);
            if (item is null) return NotFound();

            if (dto.OrderId is not null)
                item.OrderId = dto.OrderId.Value;

            if (dto.ProductId is not null)
                item.ProductId = dto.ProductId.Value;

            if (dto.Quantity is not null)
                item.Quantity = dto.Quantity.Value;

            if (dto.PriceAtTime is not null)
                item.PriceAtTime = dto.PriceAtTime.Value;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _context.OrderItems.FirstOrDefault(oi => oi.OrderItemId == id);
            if (item is null) return NotFound();

            _context.OrderItems.Remove(item);
            _context.SaveChanges();
            return NoContent();
        }
    }
}