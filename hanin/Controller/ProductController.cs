using hanin.DBContext;
using hanin.Entities;
using hanin.Service;
using hanin.ServiceIntefrace;
using Microsoft.AspNetCore.Mvc;

namespace hanin.Controller
{
    
        [ApiController]
        [Route("api/[controller]")]
        public class ProductsController : ControllerBase
        {
            private readonly ProductServiceInt _service;
            private readonly AppDbContext _context;
            public ProductsController(ProductServiceInt service, AppDbContext context)
            {
                _service = service;
                _context = context;
            }

            [HttpGet]
            public async Task<IActionResult> Get()
            {
            var result = await _service.GetAllProductsAsync();
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductEntity product)
        {
            var result = await _service.CreateProductAsync(product);

            if (!result.Success)
            {
                return BadRequest(new { message = result.Message });
            }

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ProductEntity product)
        {
            if (id != product.Id)
            {
                return BadRequest(new ServiceResponse<ProductEntity>
                {
                    Success = false,
                    Message = "ID mismatch between URL and data body."
                });
            }

            var result = await _service.UpdateProductAsync(product);

            if (!result.Success)
            {
                return result.Message.Contains("not found") ? NotFound(result) : BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.RemoveProductAsync(id);

            if (!result.Success)
            {
                return NotFound(new { message = result.Message });
            }

            return Ok(new { message = result.Message });
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetProductByIdAsync(id);
            if (!result.Success) return NotFound(result);
            return Ok(result);
        }
    }

    }

