namespace BlockChainEventStreamProcessor.Models
{
    public abstract class Transaction
    {
        public string? Type { get; set; }
        public string? TokenId { get; set; }
    }
}

