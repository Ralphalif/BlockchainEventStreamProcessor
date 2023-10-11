using System.Text.Json;
using BlockChainEventStreamProcessor.Models;

namespace BlockchainEventStreamProcessor.Helpers
{
    public class JsonTransactionDeserializer
    {
        public static List<Transaction> DeserializeTransactions(string jsonTransactions)
        {
            if (string.IsNullOrWhiteSpace(jsonTransactions))
            {
                throw new ArgumentException("JSON transactions cannot be null or empty.", nameof(jsonTransactions));
            }

            var options = new JsonSerializerOptions
            {
                Converters = { new TransactionConverter() }
            };

            if (jsonTransactions.StartsWith("["))
            {
                // If the input starts with '[', assume it's an array of transactions
                return JsonSerializer.Deserialize<List<Transaction>>(jsonTransactions, options) ?? new List<Transaction>();
            }
            else
            {
                // If not, assume it's a single transaction object
                var transaction = JsonSerializer.Deserialize<Transaction>(jsonTransactions, options)
                    ?? throw new JsonException("Invalid JSON transaction format.");

                return new List<Transaction> { transaction };
            }
        }
    }
}

