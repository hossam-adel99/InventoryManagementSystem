using Inventory_Management_System.CQRS.Products.Queries;
using Inventory_Management_System.DTO;
using Inventory_Management_System.Enums;
using Inventory_Management_System.Interfaces;
using Inventory_Management_System.Models;
using MediatR;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Inventory_Management_System.CQRS.Transactions.GetProductById
{
    public class EditTransactionCommand:IRequest<int>
    {

        public AddTransactionDTO transactionDTO { get; set; }
        public EditTransactionCommand(AddTransactionDTO dto)
        {
            transactionDTO = dto;
        }
    }

    public class EditTransactionCommandHandler : IRequestHandler<EditTransactionCommand, int>
    {
        private readonly ITransactionRepo _transactionRepo;
        public EditTransactionCommandHandler(ITransactionRepo transactionRepo)
        {
            _transactionRepo = transactionRepo;
        }

        public async Task<int> Handle(EditTransactionCommand request, CancellationToken cancellationToken)
        {
            AddTransactionDTO dto = request.transactionDTO;

            var transaction = await _transactionRepo.GetByIdAsync(dto.Id);
            if (transaction == null)
            {
                return 0;
            }

            transaction.Type = dto.Type;
            transaction.QuantityChanged = dto.QuantityChanged;
            transaction.Date = dto.Date;
            transaction.ProductId = dto.ProductId;
            transaction.WarehouseId = dto.WarehouseId;

            transaction.FromWarehouseId = dto.FromWarehouseId;
            transaction.ToWarehouseId = dto.ToWarehouseId;

            await _transactionRepo.UpdateAsync(transaction);
            await _transactionRepo.SaveAsync();
            return transaction.Id;
        }
    }
}
