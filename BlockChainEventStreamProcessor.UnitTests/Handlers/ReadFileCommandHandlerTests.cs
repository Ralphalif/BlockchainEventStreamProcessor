
using Moq;
using BlockChainEventStreamProcessor.Handlers.Commands;
using BlockChainEventStreamProcessor.Interfaces;
using BlockChainEventStreamProcessor.Models;
using System.Text.Json;

namespace BlockChainEventStreamProcessor.Tests.Handlers.Commands
{
    [TestFixture]
    public class ReadFileCommandHandlerTests
    {
        [Test]
        public void Handle_WithValidFile_ShouldProcessTransactions()
        {
            // Arrange
            var mockTransactionProcessor = new Mock<ITransactionProcessor>();
            var handler = new ReadFileCommandHandler(mockTransactionProcessor.Object);
            string validFileName = "valid_transactions.json";

            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Create a list of sample transactions
            var sampleTransactions = new List<object>
            {
                new MintTransaction { Type = "Mint", TokenId = "0x1", Address = "0xA" },
                new TransferTransaction { Type = "Transfer", TokenId = "0x2", From = "0xB", To = "0xC" }
            };

            // Serialize sample transactions to JSON
            string jsonTransactions = JsonSerializer.Serialize(sampleTransactions);

            // Act
            File.WriteAllText(validFileName, jsonTransactions); // Create the valid file
            handler.Handle(validFileName);

            // Assert
            string expectedOutput = "Read 2 transaction(s)";
            string actualOutput = consoleOutput.ToString().Trim();
            Assert.That(actualOutput, Does.Contain(expectedOutput));
            mockTransactionProcessor.Verify(
                processor => processor.ProcessTransaction(It.IsAny<Transaction>()),
                Times.Exactly(2) // Ensure ProcessTransaction is called for each transaction
            );

            // Clean up
            File.Delete(validFileName); // Delete the test file
            Console.SetOut(Console.Out);
        }

        [Test]
        public void Handle_WithInvalidFile_ShouldDisplayErrorMessage()
        {
            // Arrange
            var mockTransactionProcessor = new Mock<ITransactionProcessor>();
            var handler = new ReadFileCommandHandler(mockTransactionProcessor.Object);
            string invalidFileName = "nonexistent_file.json";

            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            handler.Handle(invalidFileName);

            // Assert
            string expectedErrorMessage = "File not found: nonexistent_file.json";
            string actualOutput = consoleOutput.ToString().Trim();
            Assert.That(actualOutput, Does.Contain(expectedErrorMessage));

            // Clean up
            Console.SetOut(Console.Out);
        }
    }
}
