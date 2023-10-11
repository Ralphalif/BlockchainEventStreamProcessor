namespace BlockChainEventStreamProcessor.Models
{
    public class BurnTransaction : Transaction
    {
        public BurnTransaction()
        {
            Type = "Burn";
        }
    }
}

