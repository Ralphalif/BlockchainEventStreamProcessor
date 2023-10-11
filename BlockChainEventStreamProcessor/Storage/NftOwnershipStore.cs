using BlockchainEventStreamProcessor.Interfaces;
using System.Text.Json;

namespace BlockChainEventStreamProcessor.Storage
{
    public class NftOwnershipStore : INftOwnershipStore
    {
        private readonly Dictionary<string, string> nftOwnership = new();
        private readonly string dataFilePath;

        public NftOwnershipStore(string dataFilePath)
        {
            this.dataFilePath = dataFilePath;

            if (File.Exists(dataFilePath))
            {
                var jsonData = File.ReadAllText(dataFilePath);
                nftOwnership.Clear();
                nftOwnership = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonData) ?? new();
            }
        }

        public void Add(string tokenId, string address)
        {
            if (!nftOwnership.ContainsKey(tokenId))
            {
                nftOwnership[tokenId] = address;
                SaveDataToFile();
            }
            else
            {
                throw new InvalidOperationException($"Token {tokenId} is owned by someone");
            }
        }

        public void Update(string tokenId, string fromAddress, string toAddress)
        {
            if (nftOwnership.TryGetValue(tokenId, out var currentOwner) && currentOwner == fromAddress)
            {
                nftOwnership[tokenId] = toAddress;
                SaveDataToFile();
            }
            else
            {
                throw new InvalidOperationException($"Token {tokenId} not owned by {fromAddress}");
            }
        }

        public void Delete(string tokenId)
        {
            if (nftOwnership.Remove(tokenId))
            {
                SaveDataToFile();
            }
            else
            {
                throw new KeyNotFoundException($"Token {tokenId} not found for deletion");
            }
        }

        public string? GetOwner(string tokenId)
        {
            if (nftOwnership.TryGetValue(tokenId, out var owner))
            {
                return owner;
            }
            else
            {
                return default;
            }
        }

        public List<string> GetNFTsByWallet(string walletAddress)
        {
            var ownedNFTs = new List<string>();

            foreach (var kvp in nftOwnership)
            {
                if (kvp.Value == walletAddress)
                {
                    ownedNFTs.Add(kvp.Key);
                }
            }

            return ownedNFTs;
        }

        public void Reset()
        {
            nftOwnership.Clear();
            SaveDataToFile();
        }

        private void SaveDataToFile()
        {
            var jsonData = JsonSerializer.Serialize(nftOwnership);
            File.WriteAllText(dataFilePath, jsonData);
        }
    }
}

