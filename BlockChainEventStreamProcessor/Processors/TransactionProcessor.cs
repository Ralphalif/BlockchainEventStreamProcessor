using BlockchainEventStreamProcessor.Interfaces;
using BlockChainEventStreamProcessor.Models;

namespace BlockChainEventStreamProcessor.Processors
{
    public class TransactionProcessor : ITransactionProcessor
    {
        private readonly INftOwnershipStore _nftOwnershipStore;

        public TransactionProcessor(INftOwnershipStore nftOwnershipStore)
        {
            _nftOwnershipStore = nftOwnershipStore;
        }

        public void ProcessTransaction(Transaction transaction)
        {
            switch (transaction.Type.ToLowerInvariant())
            {
                case "mint":
                    if (transaction is MintTransaction mintTransaction)
                    {
                        _nftOwnershipStore.Add(mintTransaction.TokenId, mintTransaction.Address);
                    }
                    break;
                case "burn":
                    if (transaction is BurnTransaction burnTransaction)
                    {
                        _nftOwnershipStore.Delete(burnTransaction.TokenId);
                    }
                    break;
                case "transfer":
                    if (transaction is TransferTransaction transferTransaction)
                    {
                        _nftOwnershipStore.Update(transferTransaction.TokenId, transferTransaction.From, transferTransaction.To);
                    }
                    break;
                default:
                    Console.WriteLine($"Unknown transaction type: {transaction.Type}");
                    break;
            }
        }
    }
}

