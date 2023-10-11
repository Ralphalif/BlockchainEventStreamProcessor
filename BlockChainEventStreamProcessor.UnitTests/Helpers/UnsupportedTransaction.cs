using BlockChainEventStreamProcessor.Models;

namespace BlockChainEventStreamProcessor.UnitTests.Helpers
{
    public class UnsupportedTransaction : Transaction
    {
        public UnsupportedTransaction()
        {
            Type = "UnsupportedType";
        }
    }

}

