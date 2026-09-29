using Microsoft.AspNetCore.Mvc;
using p1_api.DTO;
using p1_api.Models;

namespace p1_api.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly RpmNewContext _context;

        public CategoryController(RpmNewContext context)
        {
            _context = context;
        }

        private static CategoryDTO ToDto(Category category) => new CategoryDTO
        {
            CategoryId = category.CategoryId,
            CategoryName = category.CategoryName
        };

        [HttpGet]
        public IActionResult GetAll(int page = 1, int pageSize = 10)
        {
            var categories = _context.Categories
                .OrderBy(c => c.CategoryId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList()
                .Select(ToDto)
                .ToList();

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.CategoryId == id);
            if (category is null) return NotFound();
            return Ok(ToDto(category));
        }

        [HttpPost]
        public IActionResult Create(CreateCategoryDTO dto)
        {
            var category = new Category
            {
                CategoryName = dto.CategoryName
            };

            _context.Categories.Add(category);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = category.CategoryId }, ToDto(category));
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateCategoryDTO dto)
        {
            var category = _context.Categories.FirstOrDefault(c => c.CategoryId == id);
            if (category is null) return NotFound();

            category.CategoryName = dto.CategoryName;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, PatchCategoryDTO dto)
        {
            var category = _context.Categories.FirstOrDefault(c => c.CategoryId == id);
            if (category is null) return NotFound();

            if (dto.CategoryName is not null)
                category.CategoryName = dto.CategoryName;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.CategoryId == id);
            if (category is null) return NotFound();

            _context.Categories.Remove(category);
            _context.SaveChanges();
            return NoContent();
        }
    }
}