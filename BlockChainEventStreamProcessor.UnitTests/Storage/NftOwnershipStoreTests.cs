using System;
using BlockChainEventStreamProcessor.Storage;

namespace BlockChainEventStreamProcessor.UnitTests.Storage
{
    [TestFixture]
    public class NftOwnershipStoreTests
    {
        private string testDataFilePath;
        private Dictionary<string, string> testData;
        NftOwnershipStore nftOwnershipStore;

        [SetUp]
        public void Setup()
        {
            testDataFilePath = "testNftOwnershipData.json";
            testData = new Dictionary<string, string>
            {
                { "token1", "address1" },
                { "token2", "address2" },
                { "token3", "address3" }
            };
            nftOwnershipStore = new NftOwnershipStore(testDataFilePath);
            nftOwnershipStore.Reset();
            foreach (var token in testData)
            {
                nftOwnershipStore.Add(token.Key, token.Value);
            }
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            File.Delete(testDataFilePath);
        }

        [Test]
        public void Constructor_LoadsDataFromFile()
        {
            // Assert
            var ownedTokens = nftOwnershipStore.GetNFTsByWallet("address1");
            CollectionAssert.AreEqual(new[] { "token1" }, ownedTokens);
        }

        [Test]
        public void Add_AddsOwnershipData()
        {
            // Act
            nftOwnershipStore.Add("token4", "address4");

            // Assert
            var ownedTokens = nftOwnershipStore.GetNFTsByWallet("address4");
            CollectionAssert.AreEqual(new[] { "token4" }, ownedTokens);
        }

        [Test]
        public void Add_NewToken_ShouldAddToDictionary()
        {
            // Act
            nftOwnershipStore.Add("token123", "address123");

            // Assert
            Assert.AreEqual("address123", nftOwnershipStore.GetOwner("token123"));
        }

        [Test]
        public void Add_ExistingToken_ShouldNotOverwrite()
        {
            // Arrange
            nftOwnershipStore.Add("token123", "address123");

            // Act/Assert
            Assert.Throws<InvalidOperationException>(() => nftOwnershipStore.Add("token123", "newaddress"));
        }


        [Test]
        public void Update_UpdatesOwnershipData()
        {
            // Act
            nftOwnershipStore.Update("token1", "address1", "newAddress");

            // Assert
            var ownedTokensOld = nftOwnershipStore.GetNFTsByWallet("address1");
            var ownedTokensNew = nftOwnershipStore.GetNFTsByWallet("newAddress");
            CollectionAssert.IsEmpty(ownedTokensOld);
            CollectionAssert.AreEqual(new[] { "token1" }, ownedTokensNew);
        }

        [Test]
        public void Update_ThrowsInvalidOperationExceptionIfOwnershipMismatch()
        {
            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => nftOwnershipStore.Update("token1", "address2", "newAddress"));
        }

        [Test]
        public void Delete_RemovesOwnershipData()
        {
            // Act
            nftOwnershipStore.Delete("token1");

            // Assert
            var ownedTokens = nftOwnershipStore.GetNFTsByWallet("address1");
            CollectionAssert.IsEmpty(ownedTokens);
        }

        [Test]
        public void Delete_ThrowsKeyNotFoundExceptionIfTokenNotFound()
        {
            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => nftOwnershipStore.Delete("token4"));
        }

        [Test]
        public void GetOwner_ReturnsOwnerAddress()
        {
            // Act
            var owner = nftOwnershipStore.GetOwner("token1");

            // Assert
            Assert.AreEqual("address1", owner);
        }

        [Test]
        public void GetOwner_ReturnsNullIfTokenNotFound()
        {
            // Act
            var owner = nftOwnershipStore.GetOwner("nonExistentToken");

            // Assert
            Assert.IsNull(owner);
        }

        [Test]
        public void GetNFTsByWallet_ReturnsTokensForWallet()
        {
            // Act
            var ownedTokens = nftOwnershipStore.GetNFTsByWallet("address1");

            // Assert
            CollectionAssert.AreEquivalent(new[] { "token1" }, ownedTokens);
        }

        [Test]
        public void GetNFTsByWallet_ReturnsEmptyListForUnknownWallet()
        {
            // Act
            var ownedTokens = nftOwnershipStore.GetNFTsByWallet("unknownAddress");

            // Assert
            CollectionAssert.IsEmpty(ownedTokens);
        }

        [Test]
        public void Reset_ClearsOwnershipData()
        {
            // Act
            nftOwnershipStore.Reset();

            // Assert
            var ownedTokens = nftOwnershipStore.GetNFTsByWallet("address1");
            CollectionAssert.IsEmpty(ownedTokens);
        }
    }
}

