using Microsoft.AspNetCore.Mvc;
using p1_api.DTO;
using p1_api.Models;

namespace p1_api.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly RpmNewContext _context;

        public OrderController(RpmNewContext context)
        {
            _context = context;
        }

        private static OrderDTO ToDto(Order order) => new OrderDTO
        {
            OrderId = order.OrderId,
            UserId = order.UserId,
            OrderDate = order.OrderDate,
            Status = order.Status,
        };

        [HttpGet]
        public IActionResult GetAll(int page = 1, int pageSize = 10)
        {
            var orders = _context.Orders
                .OrderBy(o => o.OrderId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList()
                .Select(ToDto)
                .ToList();

            return Ok(orders);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var order = _context.Orders.FirstOrDefault(o => o.OrderId == id);
            if (order is null) return NotFound();
            return Ok(ToDto(order));
        }

        [HttpPost]
        public IActionResult Create(CreateOrderDTO dto)
        {
            var order = new Order
            {
                UserId = dto.UserId,
                OrderDate = DateTime.UtcNow,
                Status = dto.Status,
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = order.OrderId }, ToDto(order));
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateOrderDTO dto)
        {
            var order = _context.Orders.FirstOrDefault(o => o.OrderId == id);
            if (order is null) return NotFound();

            order.UserId = dto.UserId;
            order.Status = dto.Status;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, PatchOrderDTO dto)
        {
            var order = _context.Orders.FirstOrDefault(o => o.OrderId == id);
            if (order is null) return NotFound();

            if (dto.UserId is not null)
                order.UserId = dto.UserId.Value;

            if (dto.Status is not null)
                order.Status = dto.Status;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var order = _context.Orders.FirstOrDefault(o => o.OrderId == id);
            if (order is null) return NotFound();

            _context.Orders.Remove(order);
            _context.SaveChanges();
            return NoContent();
        }
    }
}