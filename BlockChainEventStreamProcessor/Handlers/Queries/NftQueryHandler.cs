using BlockchainEventStreamProcessor.Interfaces;

namespace BlockChainEventStreamProcessor.Handlers.Queries
{
    public class NFTQueryHandler : IQueryHandler<string>
    {
        private readonly INftOwnershipStore nftOwnershipStore;

        public NFTQueryHandler(INftOwnershipStore nftOwnershipStore)
        {
            this.nftOwnershipStore = nftOwnershipStore;
        }

        public void Handle(string tokenId)
        {
            if (string.IsNullOrWhiteSpace(tokenId))
            {
                Console.WriteLine("TokenId is required for the --nft query.");
                return;
            }

            var owner = nftOwnershipStore.GetOwner(tokenId);
            if (string.IsNullOrEmpty(owner))
            {
                Console.WriteLine($"Token {tokenId} is not owned by any wallet");
            }
            else
            {
                Console.WriteLine($"Token {tokenId} is owned by {owner}");
            }
        }
    }
}
