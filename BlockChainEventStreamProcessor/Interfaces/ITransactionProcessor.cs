using BlockChainEventStreamProcessor.Models;

namespace BlockchainEventStreamProcessor.Interfaces
{
    public interface ITransactionProcessor
    {
        void ProcessTransaction(Transaction transaction);
    }
}

