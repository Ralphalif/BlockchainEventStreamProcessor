namespace BlockChainEventStreamProcessor.Models
{
    public class TransferTransaction : Transaction
    {
        public TransferTransaction()
        {
            Type = "Transfer";
        }

        public required string From { get; set; }
        public required string To { get; set; }
    }
}

