using Inventory_Management_System.CQRS.Products.Queries;
using Inventory_Management_System.CQRS.Transactions.Queries;
using Inventory_Management_System.CQRS.Warehouses.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReportsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("lowstock")]
        public async Task<IActionResult> GetLowStockReport()
        {
            var products = await _mediator.Send(new GetAllProductsWithLowStockQuery());

            if (products == null)
            {
                return NotFound(new { message = $"No products with Low Stock" });
            }

            return Ok(products);
        }

        [HttpGet("lowstock/category/{catId}")]
        public async Task<IActionResult> GetLowStockByCategoryReport(int catId)
        {
            var products = await _mediator.Send(new GetAllProductsWithLowStockByCategoryQuery{categoryId= catId});

            if (products == null)
            {
                return NotFound(new { message = $"No products with Low Stock In this category" });
            }

            return Ok(products);
        }

        [HttpGet("transactions/product/{prodId}")]
        public async Task<IActionResult> GetTransactionHistoryByProduct(int prodId)
        {
            var result = await _mediator.Send(new GetAllTransactionsByProductIdQuery{ productId = prodId });
            return Ok(result);
        }
    }
}
