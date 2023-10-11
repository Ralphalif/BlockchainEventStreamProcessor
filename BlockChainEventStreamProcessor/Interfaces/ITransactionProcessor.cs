using BlockChainEventStreamProcessor.Models;

namespace BlockChainEventStreamProcessor.Interfaces
{
    public interface ITransactionProcessor
    {
        void ProcessTransaction(Transaction transaction);
    }
}

