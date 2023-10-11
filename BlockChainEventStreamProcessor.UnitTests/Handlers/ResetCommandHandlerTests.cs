using BlockChainEventStreamProcessor.Interfaces;
using BlockChainEventStreamProcessor.Handlers.Commands;
using Moq;

namespace BlockChainEventStreamProcessor.UnitTests.Handlers
{
    [TestFixture]
    public class ResetCommandHandlerTests
    {
        [Test]
        public void Handle_ValidCommand_CallsNftOwnershipStoreReset()
        {
            // Arrange
            var nftOwnershipStoreMock = new Mock<INftOwnershipStore>();

            var handler = new ResetCommandHandler(nftOwnershipStoreMock.Object);

            // Act
            handler.Handle("reset");

            // Assert
            nftOwnershipStoreMock.Verify(nos => nos.Reset(), Times.Once);
        }

        [Test]
        public void Handle_NullCommand_PrintsErrorMessage()
        {
            // Arrange
            var nftOwnershipStoreMock = new Mock<INftOwnershipStore>();

            var handler = new ResetCommandHandler(nftOwnershipStoreMock.Object);

            // Mock Console.WriteLine to capture the message
            var consoleOutput = new System.IO.StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            handler.Handle(null);

            // Assert
            StringAssert.Contains("Invalid command arguments for --reset.", consoleOutput.ToString());
        }

        [Test]
        public void Handle_InvalidCommand_PrintsErrorMessage()
        {
            // Arrange
            var nftOwnershipStoreMock = new Mock<INftOwnershipStore>();

            var handler = new ResetCommandHandler(nftOwnershipStoreMock.Object);

            // Mock Console.WriteLine to capture the message
            var consoleOutput = new System.IO.StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            handler.Handle("invalid command");

            // Assert
            StringAssert.Contains("Invalid command arguments for --reset.", consoleOutput.ToString());
        }
    }
}

