using BlockChainEventStreamProcessor.Interfaces;

namespace BlockChainEventStreamProcessor.Handlers.Commands
{
    public class ResetCommandHandler : ICommandHandler<string>
    {
        private readonly INftOwnershipStore nftOwnershipStore;

        public ResetCommandHandler(INftOwnershipStore nftOwnershipStore)
        {
            this.nftOwnershipStore = nftOwnershipStore;
        }

        public void Handle(string command)
        {
            if (command == null || command != "--reset")
            {
                Console.WriteLine("Invalid command arguments for --reset.");
                return;
            }

            nftOwnershipStore.Reset();

            Console.WriteLine("Program was reset.");
        }
    }

}

