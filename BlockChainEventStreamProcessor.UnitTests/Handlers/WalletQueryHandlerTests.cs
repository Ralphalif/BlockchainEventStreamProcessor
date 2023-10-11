using BlockChainEventStreamProcessor.Handlers.Queries;
using BlockChainEventStreamProcessor.Interfaces;
using Moq;

namespace BlockChainEventStreamProcessor.UnitTests.Handlers
{
    [TestFixture]
    public class WalletQueryHandlerTests
    {
        [Test]
        public void Handle_ValidWalletAddressWithOwnedTokens_ShouldPrintOwnedTokens()
        {
            // Arrange
            var walletAddress = "0xWalletAddress";
            var ownedTokens = new List<string> { "Token1", "Token2", "Token3" };
            var nftOwnershipStoreMock = new Mock<INftOwnershipStore>();
            nftOwnershipStoreMock.Setup(store => store.GetNFTsByWallet(walletAddress)).Returns(ownedTokens);

            var handler = new WalletQueryHandler(nftOwnershipStoreMock.Object);

            // Mock Console.WriteLine to capture the message
            var consoleOutput = new System.IO.StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            handler.Handle(walletAddress);

            // Assert
            string expectedMessage = $"Wallet {walletAddress} holds {ownedTokens.Count} Tokens:";
            Assert.True(consoleOutput.ToString().Contains(expectedMessage));
            foreach (var tokenId in ownedTokens)
            {
                Assert.True(consoleOutput.ToString().Contains(tokenId));
            }
        }

        [Test]
        public void Handle_ValidWalletAddressWithNoOwnedTokens_ShouldPrintMessage()
        {
            // Arrange
            var walletAddress = "0xWalletAddress";
            var nftOwnershipStoreMock = new Mock<INftOwnershipStore>();
            nftOwnershipStoreMock.Setup(store => store.GetNFTsByWallet(walletAddress)).Returns(new List<string>());

            var handler = new WalletQueryHandler(nftOwnershipStoreMock.Object);

            // Mock Console.WriteLine to capture the message
            var consoleOutput = new System.IO.StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            handler.Handle(walletAddress);

            // Assert
            string expectedMessage = $"Wallet {walletAddress} holds no Tokens";
            Assert.AreEqual(expectedMessage, consoleOutput.ToString().Trim());
        }

        [Test]
        public void Handle_EmptyWalletAddress_ShouldPrintErrorMessage()
        {
            // Arrange
            var walletAddress = "";
            var nftOwnershipStoreMock = new Mock<INftOwnershipStore>();
            var handler = new WalletQueryHandler(nftOwnershipStoreMock.Object);

            // Mock Console.WriteLine to capture the message
            using var consoleOutput = new System.IO.StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            handler.Handle(walletAddress);

            // Assert
            string expectedErrorMessage = "Wallet address is required for the --wallet query.";
            Assert.AreEqual(expectedErrorMessage, consoleOutput.ToString().Trim());
        }
    }

}

