using BlockChainEventStreamProcessor.Interfaces;
using BlockChainEventStreamProcessor.Models;
using BlockChainEventStreamProcessor.Processors;
using Moq;

namespace BlockChainEventStreamProcessor.UnitTests.Processors
{
    [TestFixture]
    public class TransactionProcessorTests
    {
        [Test]
        public void ProcessTransaction_ValidMintTransaction_ShouldAddToOwnershipStore()
        {
            // Arrange
            var nftOwnershipStoreMock = new Mock<INftOwnershipStore>();
            var transactionProcessor = new TransactionProcessor(nftOwnershipStoreMock.Object);

            var mintTransaction = new MintTransaction
            {
                Type = "Mint",
                TokenId = "123",
                Address = "0x123"
            };

            // Act
            transactionProcessor.ProcessTransaction(mintTransaction);

            // Assert
            nftOwnershipStoreMock.Verify(store => store.Add(mintTransaction.TokenId, mintTransaction.Address), Times.Once);
        }

        [Test]
        public void ProcessTransaction_ValidBurnTransaction_ShouldDeleteFromOwnershipStore()
        {
            // Arrange
            var nftOwnershipStoreMock = new Mock<INftOwnershipStore>();
            var transactionProcessor = new TransactionProcessor(nftOwnershipStoreMock.Object);

            var burnTransaction = new BurnTransaction
            {
                Type = "Burn",
                TokenId = "123"
            };

            // Act
            transactionProcessor.ProcessTransaction(burnTransaction);

            // Assert
            nftOwnershipStoreMock.Verify(store => store.Delete(burnTransaction.TokenId), Times.Once);
        }

        [Test]
        public void ProcessTransaction_ValidTransferTransaction_ShouldUpdateOwnershipStore()
        {
            // Arrange
            var nftOwnershipStoreMock = new Mock<INftOwnershipStore>();
            var transactionProcessor = new TransactionProcessor(nftOwnershipStoreMock.Object);

            var transferTransaction = new TransferTransaction
            {
                Type = "Transfer",
                TokenId = "123",
                From = "0x456",
                To = "0x789"
            };

            // Act
            transactionProcessor.ProcessTransaction(transferTransaction);

            // Assert
            nftOwnershipStoreMock.Verify(store => store.Update(transferTransaction.TokenId, transferTransaction.From, transferTransaction.To), Times.Once);
        }
    }
}

