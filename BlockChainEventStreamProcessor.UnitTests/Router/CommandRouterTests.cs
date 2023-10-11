using System;
using BlockChainEventStreamProcessor.Handlers.Commands;
using BlockChainEventStreamProcessor.Handlers.Queries;
using BlockChainEventStreamProcessor.Interfaces;
using BlockChainEventStreamProcessor.Processors;
using BlockChainEventStreamProcessor.Routers;
using Microsoft.Extensions.DependencyInjection;
using BlockChainEventStreamProcessor.Storage;

namespace BlockChainEventStreamProcessor.UnitTests.Router
{
    [TestFixture]
    public class CommandRouterTests
    {
        private IServiceProvider serviceProvider;

        [SetUp]
        public void SetUp()
        {
            var services = new ServiceCollection();

            services.AddSingleton<INftOwnershipStore>(provider =>
            {
                var dataFilePath = "nftOwnershipData.json";
                return new NftOwnershipStore(dataFilePath);
            });

            services.AddSingleton<ITransactionProcessor, TransactionProcessor>();
            services.AddSingleton<ICommandRouter, CommandRouter>();

            services.AddScoped<ReadInlineCommandHandler>();
            services.AddScoped<ReadFileCommandHandler>();
            services.AddScoped<ResetCommandHandler>();
            services.AddScoped<NFTQueryHandler>();
            services.AddScoped<WalletQueryHandler>();
            serviceProvider = services.BuildServiceProvider();
        }

        [Test]
        public void GetCommandHandler_ReadInlineCommand_ShouldReturnReadInlineCommandHandler()
        {
            // Arrange
            var commandRouter = new CommandRouter(serviceProvider);

            // Act
            var result = commandRouter.GetCommandHandler("--read-inline");

            // Assert
            Assert.IsInstanceOf<ReadInlineCommandHandler>(result);
        }

        [Test]
        public void GetCommandHandler_ReadFileCommand_ShouldReturnReadFileCommandHandler()
        {
            // Arrange
            var commandRouter = new CommandRouter(serviceProvider);

            // Act
            var result = commandRouter.GetCommandHandler("--read-file");

            // Assert
            Assert.IsInstanceOf<ReadFileCommandHandler>(result);
        }

        [Test]
        public void GetCommandHandler_ResetCommand_ShouldReturnResetCommandHandler()
        {
            // Arrange
            var commandRouter = new CommandRouter(serviceProvider);

            // Act
            var result = commandRouter.GetCommandHandler("--reset");

            // Assert
            Assert.IsInstanceOf<ResetCommandHandler>(result);
        }

        [Test]
        public void GetCommandHandler_UnknownCommand_ShouldReturnNull()
        {
            // Arrange
            var commandRouter = new CommandRouter(serviceProvider);

            // Act
            var result = commandRouter.GetCommandHandler("--unknown-command");

            // Assert
            Assert.IsNull(result);
        }
    }
}

