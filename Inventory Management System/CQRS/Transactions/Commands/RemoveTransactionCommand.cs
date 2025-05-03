using Inventory_Management_System.DTO;
using Inventory_Management_System.Interfaces;
using MediatR;

namespace Inventory_Management_System.CQRS.Transactions.Commands
{
    public class RemoveTransactionCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }


    public class RemoveTransactionCommandHandler : IRequestHandler<RemoveTransactionCommand, bool>
    {
        private readonly ITransactionRepo _transactionRepo;

        public RemoveTransactionCommandHandler(ITransactionRepo transactionRepo)
        {
            _transactionRepo = transactionRepo;
        }

        public async Task<bool> Handle(RemoveTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = await _transactionRepo.GetByIdAsync(request.Id);
            if (transaction == null)
            {
                return false;
            }

            transaction.IsDeleted = true;
            await _transactionRepo.UpdateAsync(transaction);
            await _transactionRepo.SaveAsync();
            return true;
        }
    }
}