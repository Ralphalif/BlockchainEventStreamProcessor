using BlockChainEventStreamProcessor.Handlers.Commands;
using BlockchainEventStreamProcessor.Interfaces;
using BlockChainEventStreamProcessor.Processors;
using BlockChainEventStreamProcessor.Routers;
using BlockChainEventStreamProcessor.Storage;
using Microsoft.Extensions.DependencyInjection;
using BlockChainEventStreamProcessor.Handlers.Queries;

namespace BlockChainEventStreamProcessor.Configuration
{
    public static class ServiceConfiguration
    {
        public static IServiceProvider ConfigureServices()
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

            return services.BuildServiceProvider();
        }
    }
}

