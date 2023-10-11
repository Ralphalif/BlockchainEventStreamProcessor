using System.Text.Json;
using BlockchainEventStreamProcessor.Helpers;
using BlockchainEventStreamProcessor.Interfaces;
using BlockChainEventStreamProcessor.Models;

namespace BlockChainEventStreamProcessor.Handlers.Commands
{
    public class ReadInlineCommandHandler : ICommandHandler<string>
    {
        private readonly ITransactionProcessor _transactionProcessor;

        public ReadInlineCommandHandler(ITransactionProcessor transactionProcessor)
        {
            _transactionProcessor = transactionProcessor;
        }

        public void Handle(string jsonTransactions)
        {
            if (string.IsNullOrWhiteSpace(jsonTransactions))
            {
                Console.WriteLine("Invalid command arguments for --read-inline.");
                return;
            }

            try
            {
                List<Transaction> transactions = JsonTransactionDeserializer.DeserializeTransactions(jsonTransactions);
                Console.WriteLine($"Read {transactions?.Count()} transaction(s)");

                foreach (var transaction in transactions)
                {
                    _transactionProcessor.ProcessTransaction(transaction);
                }
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error parsing JSON transaction: {ex.Message}");
            }
        }
    }



}

