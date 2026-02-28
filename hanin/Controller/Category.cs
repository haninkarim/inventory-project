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
        public async Task<IActionResult> Get()
        {
            var result = await _service.GetAllCategoriesAsync();
            return Ok(result);
        }
        [HttpPost]
            public async Task<IActionResult> Post([FromBody] CategoryEntity category)
            {
            var result = await _service.CreateCategoryAsync(category);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> Put(int id, [FromBody] CategoryEntity category)
            {
            if (id != category.Id)
            {
                return BadRequest(new ServiceResponse<CategoryEntity>
                {
                    Success = false,
                    Message = "ID mismatch."
                });
            }

            var result = await _service.UpdateCategoryAsync(category);

            if (!result.Success)
            {
                return result.Message.Contains("not found") ? NotFound(result) : BadRequest(result);
            }

            return Ok(result);
        }

            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
            var result = await _service.RemoveCategoryAsync(id);
            if (!result.Success) return NotFound(result);

            return Ok(result);
        }
        }
    }

