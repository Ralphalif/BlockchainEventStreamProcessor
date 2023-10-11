using BlockChainEventStreamProcessor.Interfaces;

namespace BlockChainEventStreamProcessor.Handlers.Queries
{
    public class WalletQueryHandler : IQueryHandler<string>
    {
        private readonly INftOwnershipStore nftOwnershipStore;

        public WalletQueryHandler(INftOwnershipStore nftOwnershipStore)
        {
            this.nftOwnershipStore = nftOwnershipStore;
        }

        public void Handle(string walletAddress)
        {
            if (string.IsNullOrWhiteSpace(walletAddress))
            {
                Console.WriteLine("Wallet address is required for the --wallet query.");
                return;
            }

            List<string> ownedNFTs = nftOwnershipStore.GetNFTsByWallet(walletAddress);

            if (ownedNFTs.Count == 0)
            {
                Console.WriteLine($"Wallet {walletAddress} holds no Tokens");
            }
            else
            {
                Console.WriteLine($"Wallet {walletAddress} holds {ownedNFTs.Count} Tokens:");
                foreach (var tokenId in ownedNFTs)
                {
                    Console.WriteLine(tokenId);
                }
            }
        }
    }

}

