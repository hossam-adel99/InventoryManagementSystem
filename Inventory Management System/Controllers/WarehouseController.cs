using Inventory_Management_System.CQRS.Products.Commands;
using Inventory_Management_System.CQRS.Products.Orchestrators;
using Inventory_Management_System.CQRS.Products.Queries;
using Inventory_Management_System.CQRS.Warehouses.Commands;
using Inventory_Management_System.CQRS.Warehouses.Queries;
using Inventory_Management_System.DTO;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WarehouseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWarehouseById(int id)
        {
            var warehouse = await _mediator.Send(new GetWarehouseByIdQuery{ Id = id });

            if (warehouse == null)
            {
                return NotFound(new { message = $"Warehouse with ID {id} not found" });
            }

            return Ok(warehouse);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWarehousess()
        {
            var warehouses = await _mediator.Send(new GetAllWarehousesQuery());

            if (warehouses == null)
            {
                return NotFound(new { message = $"There are no Warehouses yet" });
            }

            return Ok(warehouses);
        }

        [HttpPost]
        public async Task<IActionResult> AddWarehouse(AddWarehouseDTO warehouse)
        {
            if (ModelState.IsValid)
            {
                int NewWarehouseId = await _mediator.Send(new AddWarehouseCommand(warehouse));
                if (NewWarehouseId == 0)
                {
                    return BadRequest(ModelState);
                }

                return Ok(NewWarehouseId);
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        public async Task<IActionResult> EditWarehouse(AddWarehouseDTO warehouse)
        {
            if (ModelState.IsValid)
            {
                int NewWarehouseId = await _mediator.Send(new EditWarehouseCommand(warehouse));

                if (NewWarehouseId == 0)
                {
                    return BadRequest(ModelState);
                }

                return Ok(NewWarehouseId);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveWarehouse(int id)
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

