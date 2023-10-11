using BlockChainEventStreamProcessor.Interfaces;
using BlockChainEventStreamProcessor.Handlers.Commands;
using Moq;
using BlockChainEventStreamProcessor.Models;

namespace BlockChainEventStreamProcessor.UnitTests.Handlers
{
    [TestFixture]
    public class ReadInlineCommandHandlerTests
    {
        [Test]
        public void Handle_ValidJsonTransactions_CallsTransactionProcessor()
        {
            // Arrange
            var jsonTransactions = "[{\"Type\":\"Mint\",\"TokenId\":\"Token123\",\"Address\":\"Address123\"}]";

            var transactionProcessorMock = new Mock<ITransactionProcessor>();
            var nftOwnershipStoreMock = new Mock<INftOwnershipStore>();

            var handler = new ReadInlineCommandHandler(transactionProcessorMock.Object);

            // Act
            handler.Handle(jsonTransactions);

            // Assert
            transactionProcessorMock.Verify(tp => tp.ProcessTransaction(It.IsAny<Transaction>()), Times.Once);
        }

        [Test]
        public void Handle_InvalidJsonTransactions_PrintsErrorMessage()
        {
            // Arrange
            var invalidJsonTransactions = "invalid_json";

            var transactionProcessorMock = new Mock<ITransactionProcessor>();

            var handler = new ReadInlineCommandHandler(transactionProcessorMock.Object);

            // Mock Console.WriteLine to capture the message
            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            handler.Handle(invalidJsonTransactions);

            // Assert
            StringAssert.Contains("Error parsing JSON transaction:", consoleOutput.ToString());
        }
    }
}

