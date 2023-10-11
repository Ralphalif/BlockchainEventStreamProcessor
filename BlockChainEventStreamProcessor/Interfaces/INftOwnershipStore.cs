namespace BlockchainEventStreamProcessor.Interfaces
{
    public interface INftOwnershipStore
    {
        void Add(string tokenId, string address);
        void Update(string tokenId, string fromAddress, string toAddress);
        void Delete(string tokenId);
        void Reset();
        List<string> GetNFTsByWallet(string walletAddress);
        string? GetOwner(string tokenId);
    }
}

