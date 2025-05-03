using Inventory_Management_System.DTO;
using Inventory_Management_System.Interfaces;
using Inventory_Management_System.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Inventory_Management_System.CQRS.Transactions.Commands
{
    public class AddTransactionCommand : IRequest<int>
    {
        public AddTransactionDTO transactionDTO { get; set; }
        public AddTransactionCommand(AddTransactionDTO dto)
        {
            transactionDTO = dto;
        }
    }

    public class AddTransactionCommandHandler : IRequestHandler<AddTransactionCommand, int>
    {
        private readonly ITransactionRepo _transactionRepo;
        public AddTransactionCommandHandler(ITransactionRepo transactionRepo)
        {
            _transactionRepo = transactionRepo;
        }

        public async Task<int> Handle(AddTransactionCommand request, CancellationToken cancellationToken)
        {
            var dto = request.transactionDTO;

            var newTransaction = new InventoryTransaction
            {
                Type= dto.Type,
                QuantityChanged= dto.QuantityChanged,
                Date= dto.Date,
                ProductId= dto.ProductId,
                WarehouseId= dto.WarehouseId,
                //UserId
                FromWarehouseId= dto.FromWarehouseId,
                ToWarehouseId= dto.ToWarehouseId,
            };

            await _transactionRepo.AddAsync(newTransaction);
            await _transactionRepo.SaveAsync();
            return newTransaction.Id;
        }
    }
}
