using Inventory_Management_System.Interfaces;
using Inventory_Management_System.Models;
using MediatR;

namespace Inventory_Management_System.CQRS.Transactions.Queries
{
    public class GetAllTransactionsByProductIdQuery : IRequest<ICollection<InventoryTransaction>>
    {
        public int productId { get; set; }
    }

    public class GetAllTransactionsByProductIdQueryHandler : IRequestHandler<GetAllTransactionsByProductIdQuery, ICollection<InventoryTransaction>>
    {
        private readonly ITransactionRepo _transactionRepo;
        public GetAllTransactionsByProductIdQueryHandler(ITransactionRepo transactionRepo)
        {
            _transactionRepo = transactionRepo;
        }

        public async Task<ICollection<InventoryTransaction>> Handle(GetAllTransactionsByProductIdQuery request, CancellationToken cancellationToken)
        {
            ICollection<InventoryTransaction> TransactionList = (ICollection<InventoryTransaction>)await _transactionRepo.GetAllByProductIdAsync(request.productId);

            return TransactionList;
        }
    }
}
