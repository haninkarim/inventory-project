using hanin.Entities;
using hanin.ServiceIntefrace;
using Microsoft.AspNetCore.Mvc;

namespace hanin.Controller
{

        [ApiController]
        [Route("api/[controller]")]
        public class CategoryController : ControllerBase
        {
            private readonly CategoryServiceInt _service;
            public CategoryController(CategoryServiceInt service) => _service = service;

            [HttpGet]
            public async Task<IActionResult> Get() => Ok(await _service.GetAllCategoriesAsync());

            [HttpPost]
            public async Task<IActionResult> Post([FromBody] CategoryEntity category)
            {
                await _service.CreateCategoryAsync(category);
                return Ok(category);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> Put(int id, [FromBody] CategoryEntity category)
            {
                if (id != category.Id) return BadRequest();
                await _service.UpdateCategoryAsync(category);
                return NoContent();
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                await _service.RemoveCategoryAsync(id);
                return NoContent();
            }
        }
    }

