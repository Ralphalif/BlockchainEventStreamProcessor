namespace BlockChainEventStreamProcessor.Models
{
    public class MintTransaction : Transaction
    {
        public MintTransaction()
        {
            Type = "Mint";
        }
        public required string Address { get; set; }
    }
}

