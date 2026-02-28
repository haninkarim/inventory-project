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
                var products = await _service.GetAvailableProductsAsync();
                return Ok(products);
            }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductEntity product)
        {
            await _service.CreateProductAsync(product);
            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ProductEntity product)
        {
            if (id != product.Id) return BadRequest("ID mismatch");
            await _service.UpdateProductAsync(product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.RemoveProductAsync(id);
            return NoContent();
        }
    }

    }

