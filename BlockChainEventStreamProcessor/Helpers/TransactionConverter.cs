using System.Text.Json;
using System.Text.Json.Serialization;
using BlockChainEventStreamProcessor.Models;

namespace BlockchainEventStreamProcessor.Helpers
{
    public class TransactionConverter : JsonConverter<Transaction>
    {
        public override Transaction Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using JsonDocument doc = JsonDocument.ParseValue(ref reader);
            var root = doc.RootElement;
            var transactionType = root.GetProperty("Type").GetString();
            switch (transactionType?.ToLowerInvariant())
            {
                case "mint":
                    return JsonSerializer.Deserialize<MintTransaction>(root.GetRawText());
                case "burn":
                    return JsonSerializer.Deserialize<BurnTransaction>(root.GetRawText());
                case "transfer":
                    return JsonSerializer.Deserialize<TransferTransaction>(root.GetRawText());
                default:
                    throw new NotSupportedException($"Unsupported transaction type: {transactionType}");
            }
        }

        public override void Write(Utf8JsonWriter writer, Transaction value, JsonSerializerOptions options)
        {
            if (value is MintTransaction mintTransaction)
            {
                JsonSerializer.Serialize(writer, mintTransaction, options);
            }
            else if (value is BurnTransaction burnTransaction)
            {
                JsonSerializer.Serialize(writer, burnTransaction, options);
            }
            else if (value is TransferTransaction transferTransaction)
            {
                JsonSerializer.Serialize(writer, transferTransaction, options);
            }
            else
            {
                throw new NotSupportedException($"Unsupported transaction type: {value.Type}");
            }
        }
    }
}

