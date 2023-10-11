using BlockchainEventStreamProcessor.Helpers;
using BlockChainEventStreamProcessor.Interfaces;
using BlockChainEventStreamProcessor.Models;

namespace BlockChainEventStreamProcessor.Handlers.Commands
{
    public class ReadFileCommandHandler : ICommandHandler<string>
    {

        private readonly ITransactionProcessor _transactionProcessor;

        public ReadFileCommandHandler(ITransactionProcessor transactionProcessor)
        {
            _transactionProcessor = transactionProcessor;
        }

        public void Handle(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                Console.WriteLine("File name is required for the --read-file command.");
                return;
            }

            if (!File.Exists(fileName))
            {
                Console.WriteLine($"File not found: {fileName}");
                return;
            }

            try
            {
                string jsonTransactions = File.ReadAllText(fileName);
                List<Transaction> transactions = JsonTransactionDeserializer.DeserializeTransactions(jsonTransactions);

                Console.WriteLine($"Read {transactions?.Count()} transaction(s)");

                foreach (var transaction in transactions)
                {
                    _transactionProcessor.ProcessTransaction(transaction);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading or processing the file: {ex.Message}");
            }
        }
    }
}

