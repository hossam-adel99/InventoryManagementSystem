using Inventory_Management_System.CQRS.Products.Commands;
using Inventory_Management_System.CQRS.Stocks.Orchestrators;
using Inventory_Management_System.DTO;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IMediator _mediator;
        public StockController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddStock(AddStocktakingDTO stocktakingDTO)
        {
            if (ModelState.IsValid)
            {
                int NewStockId = await _mediator.Send(new AddStockOrchestrator(stocktakingDTO));

                if (NewStockId <= 0)
                {
                    return BadRequest(ModelState);
                }

                return Ok(NewStockId);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveStock(AddStocktakingDTO stocktakingDTO)
        {
            if (ModelState.IsValid)
            {
                int StockId = await _mediator.Send(new RemoveStockOrchestrator(stocktakingDTO));

                if (StockId <= 0)
                {
                    return BadRequest(ModelState);
                }

                return Ok(StockId);
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        public async Task<IActionResult> TransferStock([FromBody] TransfarStockDTO stocktakingDTO)
        {
            if (ModelState.IsValid)
            {
                int StockId = await _mediator.Send(new TransferStockOrchestrator(stocktakingDTO));

                if (StockId <= 0)
                {
                    return BadRequest(ModelState);
                }

                return Ok(StockId);
            }
            return BadRequest(ModelState);
        }
    }
}
