using Inventory_Management_System.DTO;
using Inventory_Management_System.Interfaces;
using Inventory_Management_System.Models;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace Inventory_Management_System.CQRS.Transactions.Queries
{
    public class GetAllTransactionsQuery:IRequest<ICollection<InventoryTransaction>>
    {
    }

    public class GetAllTransactionsQueryHandler : IRequestHandler<GetAllTransactionsQuery, ICollection<InventoryTransaction>>
    {
        private readonly ITransactionRepo _transactionRepo;
        public GetAllTransactionsQueryHandler(ITransactionRepo transactionRepo)
        {
            _transactionRepo = transactionRepo;
        }

        public async Task<ICollection<InventoryTransaction>> Handle(GetAllTransactionsQuery request, CancellationToken cancellationToken)
        {
            ICollection<InventoryTransaction> TransactionList = (ICollection<InventoryTransaction>)await _transactionRepo.GetAllAsync();
            
            return TransactionList;
        }
    }
}
