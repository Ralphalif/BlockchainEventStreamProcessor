using System.Text.Json;
using BlockchainEventStreamProcessor.Helpers;
using BlockChainEventStreamProcessor.Models;

namespace BlockChainEventStreamProcessor.UnitTests.Helpers
{
    [TestFixture]
    public class JsonTransactionDeserializerTests
    {
        [Test]
        public void DeserializeTransactions_InvalidJsonTransactions_ShouldThrowArgumentException()
        {
            // Arrange
            string invalidJsonTransactions = null;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => JsonTransactionDeserializer.DeserializeTransactions(invalidJsonTransactions));
        }

        [Test]
        public void DeserializeTransactions_DeserializeListFromJsonArray()
        {
            // Arrange
            var jsonTransactions = "[{\"Type\":\"Mint\",\"TokenId\":\"token123\",\"Address\":\"address123\"}," +
                                  "{\"Type\":\"Burn\",\"TokenId\":\"token456\"}]";

            // Act
            var transactions = JsonTransactionDeserializer.DeserializeTransactions(jsonTransactions);

            // Assert
            Assert.IsNotEmpty(transactions);
            Assert.AreEqual(2, transactions.Count);
            Assert.IsTrue(transactions[0] is MintTransaction);
            Assert.IsTrue(transactions[1] is BurnTransaction);
        }

        [Test]
        public void DeserializeTransactions_DeserializeSingleTransactionFromJsonObject()
        {
            // Arrange
            var jsonTransaction = "{\"Type\":\"Mint\",\"TokenId\":\"token123\",\"Address\":\"address123\"}";

            // Act
            var transactions = JsonTransactionDeserializer.DeserializeTransactions(jsonTransaction);

            // Assert
            Assert.IsNotEmpty(transactions);
            Assert.AreEqual(1, transactions.Count);
            Assert.IsTrue(transactions[0] is MintTransaction);
        }
    }

    [TestFixture]
    public class TransactionConverterTests
    {
        [Test]
        public void ConvertMintTransactionToJson()
        {
            var converter = new TransactionConverter();
            var mintTransaction = new MintTransaction
            {
                Type = "Mint",
                TokenId = "token123",
                Address = "address123"
            };

            var options = new JsonSerializerOptions { Converters = { converter } };

            var json = JsonSerializer.Serialize(mintTransaction, typeof(Transaction), options);

            var deserializedTransaction = JsonSerializer.Deserialize<Transaction>(json, options);

            Assert.IsInstanceOf<MintTransaction>(deserializedTransaction);
            Assert.AreEqual(mintTransaction.TokenId, ((MintTransaction)deserializedTransaction).TokenId);
            Assert.AreEqual(mintTransaction.Address, ((MintTransaction)deserializedTransaction).Address);
        }

        [Test]
        public void ConvertJsonToMintTransaction()
        {
            var converter = new TransactionConverter();
            string mintJson = "{ \"Type\": \"Mint\", \"TokenId\": \"token123\", \"Address\": \"address123\" }";

            var options = new JsonSerializerOptions { Converters = { converter } };

            var deserializedTransaction = JsonSerializer.Deserialize<Transaction>(mintJson, options);

            Assert.IsInstanceOf<MintTransaction>(deserializedTransaction);
            var deserializedMintTransaction = (MintTransaction)deserializedTransaction;
            Assert.AreEqual("token123", deserializedMintTransaction.TokenId);
            Assert.AreEqual("address123", deserializedMintTransaction.Address);
        }

        [Test]
        public void ConvertBurnTransactionToJson()
        {
            var converter = new TransactionConverter();
            var burnTransaction = new BurnTransaction
            {
                Type = "Burn",
                TokenId = "token456"
            };

            var options = new JsonSerializerOptions { Converters = { converter } };


            var json = JsonSerializer.Serialize(burnTransaction, typeof(Transaction), options);

            var deserializedTransaction = JsonSerializer.Deserialize<Transaction>(json, options);

            Assert.IsInstanceOf<BurnTransaction>(deserializedTransaction);
            Assert.AreEqual(burnTransaction.TokenId, ((BurnTransaction)deserializedTransaction).TokenId);
        }

        [Test]
        public void ConvertJsonToBurnTransaction()
        {
            var converter = new TransactionConverter();
            string burnJson = "{ \"Type\": \"Burn\", \"TokenId\": \"token456\" }";

            var options = new JsonSerializerOptions { Converters = { converter } };

            var deserializedTransaction = JsonSerializer.Deserialize<Transaction>(burnJson, options);

            Assert.IsInstanceOf<BurnTransaction>(deserializedTransaction);
            var deserializedBurnTransaction = (BurnTransaction)deserializedTransaction;
            Assert.AreEqual("token456", deserializedBurnTransaction.TokenId);
        }


        [Test]
        public void ConvertTransferTransactionToJson()
        {
            var converter = new TransactionConverter();
            var transferTransaction = new TransferTransaction
            {
                Type = "Transfer",
                TokenId = "token789",
                From = "fromAddress",
                To = "toAddress"
            };

            var options = new JsonSerializerOptions { Converters = { converter } };


            var json = JsonSerializer.Serialize(transferTransaction, typeof(Transaction), options);

            var deserializedTransaction = JsonSerializer.Deserialize<Transaction>(json, options);

            Assert.IsInstanceOf<TransferTransaction>(deserializedTransaction);
            Assert.AreEqual(transferTransaction.TokenId, ((TransferTransaction)deserializedTransaction).TokenId);
            Assert.AreEqual(transferTransaction.From, ((TransferTransaction)deserializedTransaction).From);
            Assert.AreEqual(transferTransaction.To, ((TransferTransaction)deserializedTransaction).To);
        }

        [Test]
        public void ConvertJsonToTransferTransaction()
        {
            var converter = new TransactionConverter();
            string transferJson = "{ \"Type\": \"Transfer\", \"TokenId\": \"token789\", \"From\": \"sender\", \"To\": \"receiver\" }";

            var options = new JsonSerializerOptions { Converters = { converter } };

            var deserializedTransaction = JsonSerializer.Deserialize<Transaction>(transferJson, options);

            Assert.IsInstanceOf<TransferTransaction>(deserializedTransaction);
            var deserializedTransferTransaction = (TransferTransaction)deserializedTransaction;
            Assert.AreEqual("token789", deserializedTransferTransaction.TokenId);
            Assert.AreEqual("sender", deserializedTransferTransaction.From);
            Assert.AreEqual("receiver", deserializedTransferTransaction.To);
        }

        [Test]
        public void ConvertUnsupportedTransactionToJson_ShouldThrowException()
        {
            var converter = new TransactionConverter();
            var unsupportedTransaction = new UnsupportedTransaction();

            var options = new JsonSerializerOptions { Converters = { converter } };

            Assert.Throws<NotSupportedException>(() =>
            {
                JsonSerializer.Serialize(unsupportedTransaction, typeof(Transaction), options);
            });
        }

        [Test]
        public void ConvertJsonToUnsupportedTransaction()
        {
            var converter = new TransactionConverter();
            string unsupportedJson = "{ \"Type\": \"Unknown\", \"TokenId\": \"token999\" }";

            var options = new JsonSerializerOptions { Converters = { converter } };

            Assert.Throws<NotSupportedException>(() =>
            {
                JsonSerializer.Deserialize<Transaction>(unsupportedJson, options);
            });
        }

    }

}

