using Inventory_Management_System.CQRS.Products.Queries;
using Inventory_Management_System.CQRS.Products.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Inventory_Management_System.DTO;
using Inventory_Management_System.CQRS.Products.Orchestrators;

namespace Inventory_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _mediator.Send(new GetProductByIdQuery { Id = id });

            if (product == null)
            {
                return NotFound(new { message = $"Product with ID {id} not found" });
            }

            return Ok(product);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _mediator.Send(new GetAllProductsQuery());

            if (products == null)
            {
                return NotFound(new { message = $"There are no products yet" });
            }

            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(AddProductDTO product)
        {
            if(ModelState.IsValid)
            {
                int NewProductId = await _mediator.Send(new AddProductCommand(product));
                if (NewProductId == 0)
                {
                    return BadRequest(ModelState);
                }

                return Ok(NewProductId);
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        public async Task<IActionResult> EditProduct(AddProductDTO product)
        {
            if (ModelState.IsValid)
            {
                int NewProductId = await _mediator.Send(new EditProductCommand(product));

                if (NewProductId == 0)
                {
                    return BadRequest(ModelState);
                }

                return Ok(NewProductId);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveProduct(int id)
        {
            bool response = await _mediator.Send(new RemoveWarehouseAndStocktakingOrchestrator{ Id = id });

            if (response == false)
            {
                return BadRequest(ModelState);
            }

            return Ok(response);
        }
    }
}
