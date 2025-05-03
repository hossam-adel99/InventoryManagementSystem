using Inventory_Management_System.DTO;
using Inventory_Management_System.Interfaces;
using Inventory_Management_System.Models;
using Inventory_Management_System.Repositories;
using MediatR;

namespace Inventory_Management_System.CQRS.Transactions.Queries
{
    public class GetTransactionByIdQuery : IRequest<InventoryTransaction>
    {
        public int Id { get; set; }
    }

    public class GetTransactionByIdQueryHandler : IRequestHandler<GetTransactionByIdQuery, InventoryTransaction>
    {
        private readonly ITransactionRepo _transactionRepo;
        public GetTransactionByIdQueryHandler(ITransactionRepo transactionRepo)
        {
            _transactionRepo = transactionRepo;
        }

        public async Task<InventoryTransaction?> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
        {
            InventoryTransaction transaction = await _transactionRepo.GetByIdAsync(request.Id);

            return transaction;
        }
    }
}
