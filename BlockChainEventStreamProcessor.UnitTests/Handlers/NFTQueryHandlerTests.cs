using BlockChainEventStreamProcessor.Handlers.Queries;
using BlockChainEventStreamProcessor.Interfaces;
using Moq;

namespace BlockChainEventStreamProcessor.UnitTests.Handlers
{
    [TestFixture]
    public class NFTQueryHandlerTests
    {
        [Test]
        public void Handle_ValidTokenId_ShouldPrintOwner()
        {
            // Arrange
            var tokenId = "12345";
            var owner = "0xOwnerAddress";
            var nftOwnershipStoreMock = new Mock<INftOwnershipStore>();
            nftOwnershipStoreMock.Setup(store => store.GetOwner(tokenId)).Returns(owner);

            var handler = new NFTQueryHandler(nftOwnershipStoreMock.Object);

            // Mock Console.WriteLine to capture the message
            var consoleOutput = new System.IO.StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            handler.Handle(tokenId);

            // Assert
            string expectedMessage = $"Token {tokenId} is owned by {owner}";
            Assert.AreEqual(expectedMessage, consoleOutput.ToString().Trim());
        }

        [Test]
        public void Handle_EmptyTokenId_ShouldPrintErrorMessage()
        {
            // Arrange
            var tokenId = "";
            var nftOwnershipStoreMock = new Mock<INftOwnershipStore>();
            var handler = new NFTQueryHandler(nftOwnershipStoreMock.Object);

            // Mock Console.WriteLine to capture the message
            var consoleOutput = new System.IO.StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            handler.Handle(tokenId);

            // Assert
            string expectedErrorMessage = "TokenId is required for the --nft query.";
            Assert.AreEqual(expectedErrorMessage, consoleOutput.ToString().Trim());
        }

        [Test]
        public void Handle_TokenNotOwned_ShouldPrintMessage()
        {
            // Arrange
            var tokenId = "54321";
            var nftOwnershipStoreMock = new Mock<INftOwnershipStore>();
            _ = nftOwnershipStoreMock.Setup(store => store.GetOwner(tokenId)).Returns((string)null);

            var handler = new NFTQueryHandler(nftOwnershipStoreMock.Object);

            // Mock Console.WriteLine to capture the message
            var consoleOutput = new System.IO.StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            handler.Handle(tokenId);

            // Assert
            string expectedMessage = $"Token {tokenId} is not owned by any wallet";
            Assert.AreEqual(expectedMessage, consoleOutput.ToString().Trim());
        }
    }
}

